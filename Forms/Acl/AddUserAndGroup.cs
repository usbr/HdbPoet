using System.Windows.Forms;

namespace HdbPoet.Acl
{
    public partial class AddUserAndGroup : Form
    {
        public AddUserAndGroup()
        {
            InitializeComponent();
            this.comboBoxUserName.Items.Clear();
            this.comboBoxUserName.Items.AddRange(Hdb.Instance.Server.OracleUsers);
        }

        public string Username
        {
            get { 
                //               return this.textBoxUser.Text; 
                return this.comboBoxUserName.Text;
            }
            set {
                this.comboBoxUserName.Text = value;
                //this.textBoxUser.Text = value;
            
            }
        }

        void Enabling()
        {
            button1.Enabled = Group.Trim() != "" && Username.Trim() != "";

        }
        public string Group
        {
            get { return this.comboBox1.Text; }
            set
            {
                this.comboBox1.Text = value;
            }
        }

        public void SetGroups(string[] groupNames)
        {
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.AddRange(groupNames);
        }

        private void comboBoxUserName_TextChanged(object sender, System.EventArgs e)
        {
            Enabling();
        }

        private void comboBox1_TextChanged(object sender, System.EventArgs e)
        {
            Enabling();
        }
    }
}
