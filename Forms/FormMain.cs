using System;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using HdbPoet.Properties;
using Reclamation.Core;
using System.IO;
using HdbPoet.MetaData;

namespace HdbPoet
{
    /// <summary>
    /// This is the main form for the HDB-POET application
    /// </summary>
    public class FormMain : System.Windows.Forms.Form
    {
        HdbBrowser browser;
        string filename = "Graph0";
        int untitledCounter;
        public static string[] Arguments;

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItemFile;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemHelpMain;
        private System.Windows.Forms.MenuItem menuItemAbout;
        private System.Windows.Forms.MenuItem menuItemPrint;
        private System.Windows.Forms.MenuItem menuItemNew;
        private System.Windows.Forms.MenuItem menuItemOpen;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItemSaveAs;
        private System.Windows.Forms.MenuItem menuItemSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private MenuItem menuItemLegend;
        private MenuItem menuItem2;
        private ImageList imageList1;
        private MenuItem menuAdmin;
        private MenuItem menuItemUserAdmin;
        private MenuItem menuItemGettingStarted;
        private MenuItem menuItemMetadata;
        private MenuItem menuItemLog;
        private MenuItem menuItemUpgrade;
        private MenuItem menuItemOptions;
        private System.Windows.Forms.ToolTip toolTip1;


        public FormMain()
        {
            this.Font = new Font("Sans Serif", 8.25F);
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemNew = new System.Windows.Forms.MenuItem();
            this.menuItemOpen = new System.Windows.Forms.MenuItem();
            this.menuItemSave = new System.Windows.Forms.MenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemPrint = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuAdmin = new System.Windows.Forms.MenuItem();
            this.menuItemUserAdmin = new System.Windows.Forms.MenuItem();
            this.menuItemMetadata = new System.Windows.Forms.MenuItem();
            this.menuItemHelpMain = new System.Windows.Forms.MenuItem();
            this.menuItemGettingStarted = new System.Windows.Forms.MenuItem();
            this.menuItemLegend = new System.Windows.Forms.MenuItem();
            this.menuItemLog = new System.Windows.Forms.MenuItem();
            this.menuItemOptions = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.menuItemUpgrade = new System.Windows.Forms.MenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuAdmin,
            this.menuItemHelpMain});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.menuItem3,
            this.menuItemPrint,
            this.menuItemExit});
            this.menuItemFile.Text = "&File";
            // 
            // menuItemNew
            // 
            this.menuItemNew.Index = 0;
            this.menuItemNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItemNew.Text = "&New";
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Index = 1;
            this.menuItemOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItemOpen.Text = "&Open...";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Index = 2;
            this.menuItemSave.Text = "&Save";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Index = 3;
            this.menuItemSaveAs.Text = "Save &As...";
            this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 4;
            this.menuItem3.Text = "-";
            // 
            // menuItemPrint
            // 
            this.menuItemPrint.Index = 5;
            this.menuItemPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.menuItemPrint.Text = "&Print...";
            this.menuItemPrint.Click += new System.EventHandler(this.menuItemPrint_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 6;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuAdmin
            // 
            this.menuAdmin.Index = 1;
            this.menuAdmin.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemUserAdmin,
            this.menuItemMetadata});
            this.menuAdmin.Text = "Admin";
            // 
            // menuItemUserAdmin
            // 
            this.menuItemUserAdmin.Index = 0;
            this.menuItemUserAdmin.Text = "&Users and Groups";
            this.menuItemUserAdmin.Click += new System.EventHandler(this.menuItemUserAdmin_Click);
            // 
            // menuItemMetadata
            // 
            this.menuItemMetadata.Index = 1;
            this.menuItemMetadata.Text = "&Meta Data";
            this.menuItemMetadata.Click += new System.EventHandler(this.menuItemMetadata_Click);
            // 
            // menuItemHelpMain
            // 
            this.menuItemHelpMain.Index = 2;
            this.menuItemHelpMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemGettingStarted,
            this.menuItemLegend,
            this.menuItemLog,
            this.menuItemOptions,
            this.menuItem2,
            this.menuItemAbout,
            this.menuItemUpgrade});
            this.menuItemHelpMain.Text = "&Help";
            // 
            // menuItemGettingStarted
            // 
            this.menuItemGettingStarted.Index = 0;
            this.menuItemGettingStarted.Text = "&Getting Started";
            this.menuItemGettingStarted.Click += new System.EventHandler(this.menuItemGettingStarted_Click);
            // 
            // menuItemLegend
            // 
            this.menuItemLegend.Index = 1;
            this.menuItemLegend.Text = "&Legend";
            this.menuItemLegend.Click += new System.EventHandler(this.menuItemLegend_Click);
            // 
            // menuItemLog
            // 
            this.menuItemLog.Index = 2;
            this.menuItemLog.Text = "&View Log";
            this.menuItemLog.Click += new System.EventHandler(this.menuItemLog_Click);
            // 
            // menuItemOptions
            // 
            this.menuItemOptions.Index = 3;
            this.menuItemOptions.Text = "Options";
            this.menuItemOptions.Click += new System.EventHandler(this.menuItemOptions_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 4;
            this.menuItem2.Text = "-";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 5;
            this.menuItemAbout.Text = "&About";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // menuItemUpgrade
            // 
            this.menuItemUpgrade.Index = 6;
            this.menuItemUpgrade.Text = "Check Upgrades";
            this.menuItemUpgrade.Click += new System.EventHandler(this.menuItemUpgrade_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "HDB Files |*.hdb|AllFiles|*.*";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "HDB Files |*.hdb|AllFiles|*.*";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "database_pipes_24bit.bmp");
            this.imageList1.Images.SetKeyName(3, "excelsmall.bmp");
            this.imageList1.Images.SetKeyName(4, "EXCEL_257.ico");
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 741);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(800, 700);
            this.Name = "FormMain";
            this.Text = "HDB-POET";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {

            try
            {
                Logger.EnableLogger();
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //Application.
                //MessageBox.Show("Test");
                FileUtility.CleanTempPath();

                //MessageBox.Show("temp path is ready");
                Arguments = args;

                OracleServer oracle = OracleServer.ConnectToOracle();
                //MessageBox.Show("connected attempted oracle");

                if (oracle == null)
                    return;
                Hdb.Instance = new Hdb(oracle);
                
                Application.Run(new FormMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"\n"+ex.StackTrace);
                
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string msg = "CurrentDomain_UnhandledException " + e.ExceptionObject.ToString();
            // MessageBox.Show("Please check the log \n"+msg);
        }
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            //string msg = "Application_ThreadException";
        }


       

        private void menuItemExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void menuItemAbout_Click(object sender, System.EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        private void menuItemUpgrade_Click(object sender, System.EventArgs e)
        {
            Upgrade u = new Upgrade();
            u.ShowDialog();
        }


        private void menuItemPrint_Click(object sender, System.EventArgs e)
        {
            browser.Print();
        }

        private void menuItemNew_Click(object sender, System.EventArgs e)
        {
            if (browser.NewDataSet())
            {
                browser.Visible = true;
                filename = "Graph" + untitledCounter++;
                filename += ".hdb";
                UpdateTitle();
            }
        }

        private void menuItemOpen_Click(object sender, System.EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                browser.Visible = true;
                browser.OpenFile(openFileDialog1.FileName);
                filename = openFileDialog1.FileName;
                UpdateTitle();
            }
        }

        void UpdateTitle()
        {
            this.Text = System.IO.Path.GetFileNameWithoutExtension(filename)
                + "    HDB-POET (" + Hdb.Instance.Server.ServiceName + ")  " 
                + Hdb.Instance.Server.TimeZone;
        }



        private void menuItemSave_Click(object sender, System.EventArgs e)
        {
            browser.SaveGraph(filename);
        }

        private void menuItemSaveAs_Click(object sender, System.EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                browser.SaveGraph(saveFileDialog1.FileName);
                filename = saveFileDialog1.FileName;
                UpdateTitle();
            }

        }

        private void FormMain_Load(object sender, System.EventArgs e)
        {

            browser = new HdbBrowser();
            browser.Parent = this;
            browser.Dock = DockStyle.Fill;
            browser.BringToFront();

            browser.FilenameChanged += new HdbBrowser.OnFileChanged(browser_FilenameChanged);
            //browser.Show();
            browser.Visible = false;
            if (Arguments.Length > 0)
            {
                browser.Visible = true;
                browser.OpenFile(Arguments[0]);
            }
            else
            {
                menuItemNew_Click(this, EventArgs.Empty);
            }
            browser.OnGraph += new EventHandler(browser_OnGraph);

            menuAdmin.Visible = Hdb.Instance.IsAclAdministrator;

        }

        void browser_OnGraph(object sender, EventArgs e)
        {
           // TimeSeriesDataSet ds = browser.TimeSeriesDataSet;



        }

        private void browser_FilenameChanged(object sender, HdbBrowser.HDBPoetEventArgs fe)
        {
            this.filename = fe.filename;
            UpdateTitle();
        }



        private void menuItemLegend_Click(object sender, EventArgs e)
        {
            Legend l = new Legend(false);
            if (browser.ValidationEnabled)
            {
                l = new Legend("validation");
            }
            else if (browser.QaQcEnabled)
            {
                l = new Legend("qaqc");
            }
            l.Show();
        }




        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void menuItemUserAdmin_Click(object sender, EventArgs e)
        {
            var dlg = new AclManagement();
            dlg.ShowDialog();
        }

        private void menuItemGettingStarted_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/usbr/HdbPoet/wiki");
        }

        private void menuItemMetadata_Click(object sender, EventArgs e)
        {

            var f = new FormMetaData();
            f.Show();
        }

        private void menuItemLog_Click(object sender, EventArgs e)
        {
            Logger.ViewLog();
        }

        private void menuItemOptions_Click(object sender, EventArgs e)
        {
            var f = new Options();
            f.Show();
        }
    }

}
