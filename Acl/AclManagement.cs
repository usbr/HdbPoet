using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using HdbPoet.Acl;
using System.IO;

namespace HdbPoet
{
    public partial class AclManagement : Form
    {

        TreeModel model;
       
        AclNode groups;
        AclNode rootUsers;
        public AclManagement()
        {
            InitializeComponent();
            InitTree();
        }


        private void InitTree()
        {
            //InsertFile(@"C:\TEMP\site_id_edit_list_Navajo.txt", "Navajo");

            treeViewAdv1.NodeControls.Clear();

            NodeStateIcon ni = new NodeStateIcon();
            ni.DataPropertyName = "Icon";
            treeViewAdv1.NodeControls.Add(ni);
            NodeTextBox tb = new NodeTextBox();
            tb.DataPropertyName = "Text";
            treeViewAdv1.NodeControls.Add(tb);
            treeViewAdv1.SelectionChanged += new EventHandler(treeViewAdv1_SelectionChanged);
            treeViewAdv1.SelectionMode = TreeSelectionMode.Single;

            model = new TreeModel();

            this.treeViewAdv1.Model = model;
            HdbNode root = new HdbNode(Hdb.Instance.Server.ServiceName);
            rootUsers = CreateNode("Users", AclNodeType.Folder);
            groups =CreateNode("Groups", AclNodeType.Folder);
            model.Root.Nodes.Add(rootUsers);
            model.Root.Nodes.Add(groups);

            var ref_user_groups = Hdb.Instance.Server.Table("ref_user_groups", "select User_name,group_name from ref_user_groups order by user_name");
            AddUsersToTree(ref_user_groups, rootUsers);
            AddGroupsToTree(ref_user_groups);

//            treeViewAdv1.Root.Children[0].Expand();
            //model.OnStructureChanged(new TreePathEventArgs());
            Enabling();

        }

        private AclNode CreateNode(string txt, AclNodeType type)
        {
            return CreateNode(txt, type, -1);
        }
        private AclNode CreateNode(string txt, AclNodeType type, decimal site_id)
        {
            var n = new AclNode(txt, type);
            if (type == AclNodeType.Site || type == AclNodeType.User)
            {
                n.Icon = imageList1.Images["Generic_Document.ico"];
            }
            if (type == AclNodeType.User )
            {
                n.Icon = imageList1.Images["User.ico"];
            }
            n.SiteID = site_id;
            return n;
        }

       /*
        -Groups
  --Navajo
       ---Users
        	Wsharp
        	Jstensfield
       --- Sites
•	Navajo  River bl oso Diversion dam
•	Navajo Reservoir
•	Litle Navajo River.
        */
        private void AddGroupsToTree(DataTable ref_user_groups)
        {
            var grpList = (from row in ref_user_groups.AsEnumerable()
                           select row.Field<string>("group_name")).Distinct().ToArray();

            var acl_view = Hdb.Instance.Server.Table("a", "select * from acl_view order by site_name");
            foreach (var g in grpList)
            {
                var gf = CreateNode(g, AclNodeType.GroupFolder);
                groups.Nodes.Add(gf);
             
                AclNode users = CreateNode("Users", AclNodeType.Folder);
                AclNode sites = CreateNode("Sites", AclNodeType.Folder);

                gf.Nodes.Add(users);
                gf.Nodes.Add(sites);

                AddUsersToGroup(ref_user_groups, users,g);
                // add sites in this groupFolder
                var siteList = from row in acl_view.AsEnumerable()
                                where row.Field<string>("group_name") == g
                                select  row;

                foreach (var item in siteList)
                {
                    string name = item["site_name"].ToString();
                    decimal site_id = Convert.ToDecimal(item["site_id"]);
                    var karl = CreateNode(name, AclNodeType.Site,site_id);
                    sites.Nodes.Add(karl);
                }
            }
        }

        private void AddUsersToGroup(DataTable ref_user_groups, AclNode group, string groupName)
        {
            
            var userList = (from row in ref_user_groups.AsEnumerable()
                            where row.Field<string>("group_name") == groupName
                            select row.Field<string>("user_name"));
            foreach (var u in userList)
            {
                group.Nodes.Add(CreateNode(u, AclNodeType.User));
            }
        }


        private void AddUsersToTree(DataTable ref_user_groups, AclNode parent)
        {
            var userList = (from row in ref_user_groups.AsEnumerable()
                            select row.Field<string>("user_name")).Distinct().ToArray();

            //this.users.Nodes.Clear();
            parent.Nodes.Clear();
            foreach (var u in userList)
            {
                var node = CreateNode(u, AclNodeType.User);
                var grpList = (from row in ref_user_groups.AsEnumerable()
                               where row.Field<string>("user_name") == u
                               select row.Field<string>("group_name")).Distinct().ToArray();

                parent.Nodes.Add(node);
                foreach (var grp in grpList)
                {
                    var g = CreateNode(grp, AclNodeType.Folder);
                    node.Nodes.Add(g);
                }
            }

        }

        void treeViewAdv1_SelectionChanged(object sender, EventArgs e)
        {
            Enabling();
            //throw new NotImplementedException();
        }

        private void Enabling()
        {
            if (treeViewAdv1.SelectedNodes.Count == 0)
            {
                contextMenuStrip1.Enabled = false;
            }
            else
            {
                contextMenuStrip1.Enabled = true;

                menuAddGroup.Enabled = false;
                menuDelete.Enabled = false;
                menuAddSite.Enabled = false;
                menuAddUser.Enabled = false;

                var node = treeViewAdv1.SelectedNode.Tag as AclNode;

                if (node.Text == "Users")
                {
                    menuAddUser.Enabled = true;
                }
                if (node.Text == "Sites")
                {
                    menuAddSite.Enabled = true;
                }
                if (node.Text == "Groups")
                {
                    menuAddGroup.Enabled = true;
                }

                if (node.AclNodeType == AclNodeType.User)
                {
                    menuDelete.Enabled = true;
                }
                if (node.AclNodeType == AclNodeType.GroupFolder)
                {
                    menuDelete.Enabled = true;
                }

                if (node.AclNodeType == AclNodeType.Site)
                {
                    menuDelete.Enabled = true;
                }

            }
        }

        private void menuAddGroup_Click(object sender, EventArgs e)
        {
            var node = treeViewAdv1.SelectedNode.Tag as AclNode;
            if (node.Text == "Groups")
            {
                AddUserAndGroup dlg = new AddUserAndGroup();
                dlg.SetGroups(Hdb.Instance.AclGroupNames());
               // dlg.Username = node.Text;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Hdb.Instance.AclAddUserAndGroup(dlg.Username, dlg.Group);
                    node.Nodes.Add(CreateNode(dlg.Group, AclNodeType.GroupFolder));
                }
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            var node = treeViewAdv1.SelectedNode.Tag as AclNode;
            if (node.AclNodeType == AclNodeType.GroupFolder)
            {
                if (MessageBox.Show("Delete Group?", "Delete", MessageBoxButtons.YesNo)
                   == DialogResult.Yes)
                {
                    Hdb.Instance.AclDeleteGroup(node.Text);
                    node.Parent.Nodes.Remove(node);
                }
            }
            else 
                if (node.AclNodeType == AclNodeType.User)
            {
                if (MessageBox.Show("Delete User?", "Delete", MessageBoxButtons.YesNo)
                    == DialogResult.Yes)
                {
                    Hdb.Instance.AclDeleteUser(node.Text);
                    node.Parent.Nodes.Remove(node);
                }
            }
            else if (node.AclNodeType == AclNodeType.Site)
            {
                Hdb.Instance.AclDeleteSite(node.SiteID, node.Parent.Parent.Text);
                node.Parent.Nodes.Remove(node);
            }
        }

        private void menuRefresh_Click(object sender, EventArgs e)
        {
            InitTree();
        }

        private void addSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeViewAdv1.SelectedNode.Tag as AclNode;
            if (node.Text == "Sites")
            {
                var dlg = new AddSitesToGroup();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataRow row in dlg.SiteDataTable.Rows)
                    {
                        if( Convert.ToBoolean(row["Selected"]))
                        {
                            var id = Convert.ToDecimal(row["site_id"]);
                        Hdb.Instance.AclAddSite(id, node.Parent.Text);
                        node.Nodes.Add(CreateNode(row["site_name"].ToString(), AclNodeType.Site));
                        }
                    }
                }
            }
        }

        //private void toolStripButton1_Click(object sender, EventArgs e)
        //{
        //    //InsertFile(@"C:\TEMP\poet\site_id_edit_list_GJ.txt","GJ");
        //    //InsertFile(@"C:\TEMP\poet\site_id_edit_list_Navajo.txt", "Navajo");
        //    // insert text file.
        //}

        private void InsertFile(string file, string groupName)
        {
            string[] lines = File.ReadAllLines(file);
            foreach (string s in lines)
            {
                decimal site_id;
                if (decimal.TryParse(s, out site_id))
                {
                    Hdb.Instance.AclAddSite(site_id, groupName);
                }
            }
        }

        private void menuAddUser_Click(object sender, EventArgs e)
        {
            var users = treeViewAdv1.SelectedNode.Tag as AclNode;
            AclNode group = users.Parent as AclNode;

            AddUserAndGroup dlg = new AddUserAndGroup();
            dlg.SetGroups(Hdb.Instance.AclGroupNames());
            dlg.Username = "";
            if( group != null)
               dlg.Group = group.Text;

            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Hdb.Instance.AclAddUserAndGroup(dlg.Username, dlg.Group);
                var usr = CreateNode(dlg.Username, AclNodeType.User);
                users.Nodes.Add(usr);
            }
        }
    }
}
