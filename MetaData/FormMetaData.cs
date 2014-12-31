using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace HdbPoet.MetaData
{
    /// <summary>
    /// ability to edit Meta Data tables
    /// http://uchdb2.uc.usbr.gov:7777/pls/apex/f?p=101:1
    /// </summary>
    public partial class FormMetaData : Form
    {
        public FormMetaData()
        {
            InitializeComponent();
            SqlViewEditor.Server = Hdb.Instance.Server;

            HdbDataType();
            HdbSite();
            RefExtSiteDataMap();
            RefRes();
            HdbAttr();
            RefSiteAttributes();

        }

        private void RefSiteAttributes()
        {
            ViewRefSiteAttributes.SetQueries("ref_site_attr", "select * from ref_site_attr ",
                "select r.site_id, s.site_common_name, r.attr_id, r.effective_start_date_time, "
 +" r.effective_end_date_time, r.value, r.string_value,  "
+ " r.date_value, r.date_time_loaded "
+ " FROM ref_site_attr r,  hdb_site s "
+ " WHERE r.site_id = s.site_id ");
            ViewRefSiteAttributes.AddDropDownColumn("hdb_attr", "attr_id", "attr_common_name", 2, true);

        }

        private void HdbAttr()
        {
            ViewHdb_attr.SetQueries("hdb_attr", "select * from hdb_attr ", "select * from hdb_attr ",false);
            ViewHdb_attr.AddDropDownColumn("hdb_unit", "unit_id", "unit_common_name", 5, true);
        }

        private void RefRes()
        {
            edit_ref_res.SetQueries("ref_res", "select SITE_ID, DAMTYPE_ID, AGEN_ID,OFF_ID, CONSTN_PRD,CLOSE_DATE, "
+ " AREARES, CAPACT,CAPDED,CAPINAC,CAPJNT,CAPLIV,CAPSUR,CAPTOT,CHLCAP,CSTLN, "
+ " DAMVOL,ELEVCST,ELEVMINP,ELEVTAC,ELEVTIC,ELEVTDC, "
+ " ELEVTJUC,ELEVSB,ELEVTEF,FLDCTRL,RELMAX,RELMIN,RELMAXO,RELMAXP, "
+ "SPLMAX,SPLWSLELEV,STRHT, WSEMAX from ref_res",
                //d.damtype_name, a.agen_name,o.off_name ,
"select r.SITE_ID, s.site_common_name, r.DAMTYPE_ID, r.AGEN_ID,  r.OFF_ID,  CONSTN_PRD,CLOSE_DATE, "
+ " AREARES, CAPACT,CAPDED,CAPINAC,CAPJNT,CAPLIV,CAPSUR,CAPTOT,CHLCAP,CSTLN, "
+ " DAMVOL,ELEVCST,ELEVMINP,ELEVTAC,ELEVTIC,ELEVTDC, "
+ " ELEVTJUC,ELEVSB,ELEVTEF,FLDCTRL,RELMAX,RELMIN,RELMAXO,RELMAXP, "
+ " SPLMAX,SPLWSLELEV,STRHT, WSEMAX from ref_res r, hdb_site s " //, hdb_damtype d, HDB_AGEN a, HDB_USBR_OFF o "
+ " WHERE r.site_id =  s.site_id ");
            //+ " and r.damtype_id = d.damtype_id "
            //+ " and r.agen_id = a.agen_id "
            //+ " and r.off_id = o.off_id ");

            edit_ref_res.AddDropDownColumn("hdb_damtype", "damtype_id", "damtype_name", 2, true);
            edit_ref_res.AddDropDownColumn("hdb_agen", "agen_id", "agen_name", 3, true);
            edit_ref_res.AddDropDownColumn("hdb_usbr_off", "off_id", "off_name", 4, true);
        }

        private void RefExtSiteDataMap()
        {


            editRefExtMap.SetQueries("ref_ext_site_data_map", "select mapping_id, ext_data_source_id, primary_site_code, primary_data_code, "
 + " extra_keys_y_n, hdb_site_datatype_id , hdb_interval_name, hdb_method_id, hdb_computation_id, "
 + " hdb_agen_id, is_active_y_n, cmmnt, date_time_loaded "
 + " from ref_ext_site_data_map ",
                //ds.source_name,
 "select r.mapping_id, r.ext_data_source_id, r.primary_site_code, r.primary_data_code, "
+ " r.extra_keys_y_n, r.hdb_site_datatype_id , s.site_name, d.datatype_name, r.hdb_interval_name, "
+ " r.hdb_method_id, r.hdb_computation_id, c.computation_name, "
+ " r.hdb_agen_id,  r.is_active_y_n, r.cmmnt, r.date_time_loaded "
+ " from ref_ext_site_data_map r, hdb_site_datatype sdi, "
+ " hdb_site s, hdb_datatype d, cp_computation c"
+ " where r.hdb_site_datatype_id = sdi.site_datatype_id "
+ "  and sdi.site_id(+) = s.site_id"
+ "  and d.datatype_id(+) = sdi.site_datatype_id "
+ "  and c.computation_id(+) = r.hdb_computation_id ");

            editRefExtMap.AddDropDownColumn("hdb_ext_data_source", "ext_data_source_id", "ext_data_source_name", 1, true);
            editRefExtMap.AddDropDownColumn("hdb_agen", "agen_id hdb_agen_id", "agen_name", 13, false);

            ViewKeyVal.SetQueries("ref_ext_site_data_map_keyval", "select * from ref_ext_site_data_map_keyval", "select * from ref_ext_site_data_map_keyval");

        }

        private void HdbSite()
        {
            editSites.SetQueries("hdb_site", "select * from hdb_site ", "select * from hdb_site ",false);
            editSites.AddDropDownColumn("hdb_objecttype", "objecttype_id", "objecttype_name", 3, true);
            editSites.AddDropDownColumn("hdb_state", "state_id", "state_code", 5, true);
        }

        private void HdbDataType()
        {
            editDataTypes.SetQueries("hdb_datatype", "select * from hdb_datatype", "select * from hdb_datatype");
            editDataTypes.AddDropDownColumn("hdb_unit", "unit_id", "unit_common_name", 4);
            editDataTypes.AddDropDownColumn("hdb_physical_quantity", "physical_quantity_name", "physical_quantity_name", 3, true);


            editSiteDataType.SetQueries("hdb_site_datatype", "select site_datatype_id, site_id, datatype_id from hdb_site_datatype ",
                           " select "
            + " sdi.site_datatype_id, sdi.site_id,  sdi.datatype_id, "
            + " s.site_name || '  '||d.datatype_name search_string , u.unit_name  "
            + " from "
            + " hdb_site_datatype sdi, hdb_site s, hdb_datatype d, hdb_unit u, hdb_objecttype o where "
            + "         sdi.site_id = s.site_id "
            + " and    sdi.datatype_id = d.datatype_id "
            + " and    s.objecttype_id = o.objecttype_id "
            + " and    d.unit_id = u.unit_id");
            editSiteDataType.AddDropDownColumn("hdb_site", "site_id", "site_name", 1);
            editSiteDataType.AddDropDownColumn("hdb_datatype", "datatype_id", "datatype_name", 3);
        }

        [STAThread]
        static void Main(string[] args)
        {
            // Test Code.
            var svr = new OracleServer("ktarbet", File.ReadAllLines(@"C:\temp\karl.txt")[0], "uchdb2.uc.usbr.gov", "uchdb2.uc.usbr.gov", "MST", "1521");
            Hdb.Instance = new Hdb(svr);

            SqlViewEditor.Server = Hdb.Instance.Server;
            var f = new FormMetaData();
            Application.Run(f);
        }

        RatingTableMain rtm;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabPageRatingTables)
            {
                if (rtm == null)
                {
                    rtm = new RatingTableMain();
                    rtm.Parent = tabPageRatingTables;
                    rtm.BringToFront();
                    rtm.Dock = DockStyle.Fill;
                }

                rtm.LoadTableList();
            }

        }
    }
}
