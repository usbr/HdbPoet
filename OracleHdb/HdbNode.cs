using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Aga.Controls.Tree;

namespace HdbPoet
{

    
    public class HdbNode:Node
    {

        public HdbNode(string text): base(text)
        {
           
        }

        private Image _icon;

        public Image Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

       


    }
}
