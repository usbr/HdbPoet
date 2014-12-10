using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace HdbPoet
{
    public partial class Legend : Form
    {
        public Legend(bool validation)
        {
            InitializeComponent();

            try
            {

                BuildLegend(validation);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        private  void BuildLegend(bool validation)
        {
            
            string s = ConfigurationManager.AppSettings["SourceLegend"];

            if( validation)
                s= ConfigurationManager.AppSettings["ValidationLegend"];

            string[] pairs = s.Split(',');

            for (int i = 0; i < pairs.Length; i++)
            {

                string color = pairs[i].Split(':')[1];
                Console.WriteLine(color);
                string text = pairs[i].Split(':')[0];
                dataGridView1.Rows.Add(text);
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromName(color);

                
                Console.WriteLine(Color.FromName(color).Name);
            }
            dataGridView1.Rows.Add("");
            dataGridView1.CurrentCell = dataGridView1[0, pairs.Length];
            button1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
