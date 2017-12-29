namespace HdbPoet.MetaData
{
    partial class FormMetaData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMetaData));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.editSites = new HdbPoet.MetaData.SqlViewEditor();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.editSiteDataType = new HdbPoet.MetaData.SqlViewEditor();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.editDataTypes = new HdbPoet.MetaData.SqlViewEditor();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ViewKeyVal = new HdbPoet.MetaData.SqlViewEditor();
            this.editRefExtMap = new HdbPoet.MetaData.SqlViewEditor();
            this.ref_res = new System.Windows.Forms.TabPage();
            this.edit_ref_res = new HdbPoet.MetaData.SqlViewEditor();
            this.tabPageHdbAttr = new System.Windows.Forms.TabPage();
            this.ViewHdb_attr = new HdbPoet.MetaData.SqlViewEditor();
            this.tabPageRefSiteAttributes = new System.Windows.Forms.TabPage();
            this.ViewRefSiteAttributes = new HdbPoet.MetaData.SqlViewEditor();
            this.tabPageRatingTables = new System.Windows.Forms.TabPage();
            this.tabPageRefSiteCoefficients = new System.Windows.Forms.TabPage();
            this.ViewRefSiteCoefficients = new HdbPoet.MetaData.SqlViewEditor();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.ref_res.SuspendLayout();
            this.tabPageHdbAttr.SuspendLayout();
            this.tabPageRefSiteAttributes.SuspendLayout();
            this.tabPageRefSiteCoefficients.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.ref_res);
            this.tabControl1.Controls.Add(this.tabPageHdbAttr);
            this.tabControl1.Controls.Add(this.tabPageRefSiteAttributes);
            this.tabControl1.Controls.Add(this.tabPageRefSiteCoefficients);
            this.tabControl1.Controls.Add(this.tabPageRatingTables);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(757, 623);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.editSites);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(749, 597);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "sites";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // editSites
            // 
            this.editSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editSites.Location = new System.Drawing.Point(3, 3);
            this.editSites.Name = "editSites";
            this.editSites.Size = new System.Drawing.Size(743, 591);
            this.editSites.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.editSiteDataType);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(749, 597);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "site datatype";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // editSiteDataType
            // 
            this.editSiteDataType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editSiteDataType.Location = new System.Drawing.Point(3, 3);
            this.editSiteDataType.Name = "editSiteDataType";
            this.editSiteDataType.Size = new System.Drawing.Size(743, 591);
            this.editSiteDataType.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.editDataTypes);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(749, 597);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "datatypes";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // editDataTypes
            // 
            this.editDataTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editDataTypes.Location = new System.Drawing.Point(3, 3);
            this.editDataTypes.Name = "editDataTypes";
            this.editDataTypes.Size = new System.Drawing.Size(743, 591);
            this.editDataTypes.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ViewKeyVal);
            this.tabPage4.Controls.Add(this.editRefExtMap);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(749, 597);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "ref ext site_data_map";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ViewKeyVal
            // 
            this.ViewKeyVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewKeyVal.Location = new System.Drawing.Point(3, 355);
            this.ViewKeyVal.Name = "ViewKeyVal";
            this.ViewKeyVal.Size = new System.Drawing.Size(743, 239);
            this.ViewKeyVal.TabIndex = 2;
            // 
            // editRefExtMap
            // 
            this.editRefExtMap.Dock = System.Windows.Forms.DockStyle.Top;
            this.editRefExtMap.Location = new System.Drawing.Point(3, 3);
            this.editRefExtMap.Name = "editRefExtMap";
            this.editRefExtMap.Size = new System.Drawing.Size(743, 352);
            this.editRefExtMap.TabIndex = 1;
            // 
            // ref_res
            // 
            this.ref_res.Controls.Add(this.edit_ref_res);
            this.ref_res.Location = new System.Drawing.Point(4, 22);
            this.ref_res.Name = "ref_res";
            this.ref_res.Padding = new System.Windows.Forms.Padding(3);
            this.ref_res.Size = new System.Drawing.Size(749, 597);
            this.ref_res.TabIndex = 4;
            this.ref_res.Text = "ref_res";
            this.ref_res.UseVisualStyleBackColor = true;
            // 
            // edit_ref_res
            // 
            this.edit_ref_res.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edit_ref_res.Location = new System.Drawing.Point(3, 3);
            this.edit_ref_res.Name = "edit_ref_res";
            this.edit_ref_res.Size = new System.Drawing.Size(743, 591);
            this.edit_ref_res.TabIndex = 2;
            // 
            // tabPageHdbAttr
            // 
            this.tabPageHdbAttr.Controls.Add(this.ViewHdb_attr);
            this.tabPageHdbAttr.Location = new System.Drawing.Point(4, 22);
            this.tabPageHdbAttr.Name = "tabPageHdbAttr";
            this.tabPageHdbAttr.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHdbAttr.Size = new System.Drawing.Size(749, 597);
            this.tabPageHdbAttr.TabIndex = 5;
            this.tabPageHdbAttr.Text = "hdb_attr";
            this.tabPageHdbAttr.UseVisualStyleBackColor = true;
            // 
            // ViewHdb_attr
            // 
            this.ViewHdb_attr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewHdb_attr.Location = new System.Drawing.Point(3, 3);
            this.ViewHdb_attr.Name = "ViewHdb_attr";
            this.ViewHdb_attr.Size = new System.Drawing.Size(743, 591);
            this.ViewHdb_attr.TabIndex = 0;
            // 
            // tabPageRefSiteAttributes
            // 
            this.tabPageRefSiteAttributes.Controls.Add(this.ViewRefSiteAttributes);
            this.tabPageRefSiteAttributes.Location = new System.Drawing.Point(4, 22);
            this.tabPageRefSiteAttributes.Name = "tabPageRefSiteAttributes";
            this.tabPageRefSiteAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRefSiteAttributes.Size = new System.Drawing.Size(749, 597);
            this.tabPageRefSiteAttributes.TabIndex = 6;
            this.tabPageRefSiteAttributes.Text = "ref_site_attributes";
            this.tabPageRefSiteAttributes.UseVisualStyleBackColor = true;
            // 
            // ViewRefSiteAttributes
            // 
            this.ViewRefSiteAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewRefSiteAttributes.Location = new System.Drawing.Point(3, 3);
            this.ViewRefSiteAttributes.Name = "ViewRefSiteAttributes";
            this.ViewRefSiteAttributes.Size = new System.Drawing.Size(743, 591);
            this.ViewRefSiteAttributes.TabIndex = 1;
            // 
            // tabPageRatingTables
            // 
            this.tabPageRatingTables.Location = new System.Drawing.Point(4, 22);
            this.tabPageRatingTables.Name = "tabPageRatingTables";
            this.tabPageRatingTables.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRatingTables.Size = new System.Drawing.Size(749, 597);
            this.tabPageRatingTables.TabIndex = 7;
            this.tabPageRatingTables.Text = "rating tables";
            this.tabPageRatingTables.UseVisualStyleBackColor = true;
            // 
            // tabPageRefSiteCoefficients
            // 
            this.tabPageRefSiteCoefficients.Controls.Add(this.ViewRefSiteCoefficients);
            this.tabPageRefSiteCoefficients.Location = new System.Drawing.Point(4, 22);
            this.tabPageRefSiteCoefficients.Name = "tabPageRefSiteCoefficients";
            this.tabPageRefSiteCoefficients.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRefSiteCoefficients.Size = new System.Drawing.Size(749, 597);
            this.tabPageRefSiteCoefficients.TabIndex = 8;
            this.tabPageRefSiteCoefficients.Text = "ref_site_coef";
            this.tabPageRefSiteCoefficients.UseVisualStyleBackColor = true;
            // 
            // ViewRefSiteCoefficients
            // 
            this.ViewRefSiteCoefficients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewRefSiteCoefficients.Location = new System.Drawing.Point(3, 3);
            this.ViewRefSiteCoefficients.Name = "ViewRefSiteCoefficients";
            this.ViewRefSiteCoefficients.Size = new System.Drawing.Size(743, 591);
            this.ViewRefSiteCoefficients.TabIndex = 2;
            // 
            // FormMetaData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 623);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMetaData";
            this.Text = "hdb meta data editor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ref_res.ResumeLayout(false);
            this.tabPageHdbAttr.ResumeLayout(false);
            this.tabPageRefSiteAttributes.ResumeLayout(false);
            this.tabPageRefSiteCoefficients.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private SqlViewEditor editDataTypes;
        private SqlViewEditor editSiteDataType;
        private SqlViewEditor editSites;
        private System.Windows.Forms.TabPage tabPage4;
        private SqlViewEditor editRefExtMap;
        private System.Windows.Forms.TabPage ref_res;
        private SqlViewEditor edit_ref_res;
        private System.Windows.Forms.TabPage tabPageHdbAttr;
        private SqlViewEditor ViewHdb_attr;
        private System.Windows.Forms.TabPage tabPageRefSiteAttributes;
        private SqlViewEditor ViewRefSiteAttributes;
        private SqlViewEditor ViewKeyVal;
        private System.Windows.Forms.TabPage tabPageRatingTables;
        private System.Windows.Forms.TabPage tabPageRefSiteCoefficients;
        private SqlViewEditor ViewRefSiteCoefficients;
    }
}