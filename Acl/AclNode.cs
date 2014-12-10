using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Aga.Controls.Tree;

namespace HdbPoet
{

    public enum AclNodeType { User, /*UsersGroup,*/ GroupFolder, Folder,Site };
    
    public class AclNode:Node
    {
        public AclNodeType AclNodeType { get; set; }
        public decimal SiteID;
        public AclNode(string text, AclNodeType type): base(text)
        {
            SiteID = -1;
            this.AclNodeType = type;
        }
        public AclNode(string text, AclNodeType type, decimal site_id)
            : base(text)
        {
            SiteID = site_id;
            this.AclNodeType = type;
        }
        private Image _icon;

        public Image Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

       


    }
}
