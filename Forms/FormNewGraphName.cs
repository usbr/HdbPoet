using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HdbPoet
{
    public partial class FormNewGraphName : Form
    {
        public FormNewGraphName()
        {
            InitializeComponent();
        }

        public string Value
        {
            get { return this.textBoxName.Text; }
        }
    }
}
