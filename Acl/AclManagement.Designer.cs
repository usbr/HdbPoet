namespace HdbPoet
{
    partial class AclManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AclManagement));
            this.treeViewAdv1 = new Aga.Controls.Tree.TreeViewAdv();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuAddGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddSite = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddUser = new System.Windows.Forms.ToolStripButton();
            this.menuRefresh = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuAddUser = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeViewAdv1.DefaultToolTipProvider = null;
            this.treeViewAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv1.Location = new System.Drawing.Point(0, 25);
            this.treeViewAdv1.Model = null;
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.Size = new System.Drawing.Size(490, 324);
            this.treeViewAdv1.TabIndex = 0;
            this.treeViewAdv1.Text = "treeViewAdv1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddUser,
            this.menuAddGroup,
            this.menuAddSite,
            this.menuDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 114);
            // 
            // menuAddGroup
            // 
            this.menuAddGroup.Name = "menuAddGroup";
            this.menuAddGroup.Size = new System.Drawing.Size(152, 22);
            this.menuAddGroup.Text = "&Add Group";
            this.menuAddGroup.Click += new System.EventHandler(this.menuAddGroup_Click);
            // 
            // menuAddSite
            // 
            this.menuAddSite.Name = "menuAddSite";
            this.menuAddSite.Size = new System.Drawing.Size(152, 22);
            this.menuAddSite.Text = "&Add Sites";
            this.menuAddSite.Click += new System.EventHandler(this.addSiteToolStripMenuItem_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(152, 22);
            this.menuDelete.Text = "&Delete";
            this.menuDelete.Click += new System.EventHandler(this.menuDelete_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 349);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(490, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddUser,
            this.menuRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(490, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAddUser
            // 
            this.toolStripButtonAddUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAddUser.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddUser.Image")));
            this.toolStripButtonAddUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddUser.Name = "toolStripButtonAddUser";
            this.toolStripButtonAddUser.Size = new System.Drawing.Size(55, 22);
            this.toolStripButtonAddUser.Text = "Add User";
            this.toolStripButtonAddUser.Visible = false;
            // 
            // menuRefresh
            // 
            this.menuRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuRefresh.Image = ((System.Drawing.Image)(resources.GetObject("menuRefresh.Image")));
            this.menuRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuRefresh.Name = "menuRefresh";
            this.menuRefresh.Size = new System.Drawing.Size(49, 22);
            this.menuRefresh.Text = "Refresh";
            this.menuRefresh.Click += new System.EventHandler(this.menuRefresh_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Generic_Document.ico");
            this.imageList1.Images.SetKeyName(1, "user.ico");
            // 
            // menuAddUser
            // 
            this.menuAddUser.Name = "menuAddUser";
            this.menuAddUser.Size = new System.Drawing.Size(152, 22);
            this.menuAddUser.Text = "Add &User";
            this.menuAddUser.Click += new System.EventHandler(this.menuAddUser_Click);
            // 
            // AclManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 371);
            this.Controls.Add(this.treeViewAdv1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "AclManagement";
            this.Text = "Users and Groups";
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuAddGroup;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddUser;
        private System.Windows.Forms.ToolStripButton menuRefresh;
        private System.Windows.Forms.ToolStripMenuItem menuAddSite;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem menuAddUser;
    }
}