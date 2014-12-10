using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace HdbPoet
{
    /// <summary>
    /// Summary description for DateSelector.
    /// </summary>
    public class DateSelector : System.Windows.Forms.UserControl
    {
        bool m_showTime;


        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.RadioButton radioButtonXToToday;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.RadioButton radioButtonFromXToY;
        public System.Windows.Forms.NumericUpDown numericUpDownDays;
        public System.Windows.Forms.DateTimePicker dateTimePickerFrom2;
        private System.Windows.Forms.RadioButton radioButtonPreviousXDays;
        private System.Windows.Forms.Label label13;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public DateSelector()
        {
            InitializeComponent();
        }

        public void SaveToDataSet(GraphData dataSet)
        {

            TimeSeriesDataSet.GraphRow graphsRow = dataSet.GraphRow;

            graphsRow.BeginningDate = this.dateTimePickerFrom.Value;
            graphsRow.BeginningDate2 = this.dateTimePickerFrom2.Value;
            graphsRow.EndingDate = this.dateTimePickerTo.Value;
            graphsRow.PreviousDays = (int)this.numericUpDownDays.Value;
            graphsRow.ShowTime = this.m_showTime;
            graphsRow.TimeWindowType = this.TimeWindow;
        }

        public void ReadFromDataSet(GraphData dataSet)
        {
            var graphsRow = dataSet.GraphRow;
            dataSet.SetDefaults();
            // time window selection..
            this.dateTimePickerFrom.Value = graphsRow.BeginningDate;
            this.dateTimePickerFrom2.Value = graphsRow.BeginningDate2;
            this.dateTimePickerTo.Value = graphsRow.EndingDate;
            this.numericUpDownDays.Value = graphsRow.PreviousDays;
            this.TimeWindow = graphsRow.TimeWindowType;
        }


        /// <summary>
        /// Returns or sets 1 of 3 possible strings:
        /// previousXDays, FromXToY, FromXToToday
        /// </summary>
        string TimeWindow
        {
            get
            {
                if (this.radioButtonFromXToY.Checked)
                    return "FromXToY";
                else
                    if (this.radioButtonPreviousXDays.Checked)
                        return "previousXDays";
                    else
                        if (this.radioButtonXToToday.Checked)
                            return "FromXToToday";

                return "Unknown";
            }
            set
            {
                if (value == "FromXToY")
                    this.radioButtonFromXToY.Checked = true;
                else
                    if (value == "previousXDays")
                        this.radioButtonPreviousXDays.Checked = true;
                    else
                        if (value == "FromXToToday")
                            this.radioButtonXToToday.Checked = true;
                        else
                            throw new Exception("Error : " + value.ToString() + " is not defined");
            }
        }

        public bool ShowTime
        {
            get
            {
                return (m_showTime);
            }
            set
            {
                m_showTime = value;
                FormatDateTime(value);
            }
        }

        private void FormatDateTime(bool value)
        {
            if (value)
            {
                this.dateTimePickerFrom.CustomFormat = TimeSeriesConstants.TimeFormat;
                this.dateTimePickerFrom2.CustomFormat = TimeSeriesConstants.TimeFormat;
                this.dateTimePickerTo.CustomFormat = TimeSeriesConstants.TimeFormat;
            }
            else
            {
                this.dateTimePickerFrom.CustomFormat = TimeSeriesConstants.DateFormat;
                this.dateTimePickerFrom2.CustomFormat = TimeSeriesConstants.DateFormat;
                this.dateTimePickerTo.CustomFormat = TimeSeriesConstants.DateFormat;
            }
        }

        public event EventHandler<EventArgs> ValueChanged;

        private void SomethingChanged(object sender, System.EventArgs e)
        {
            EnableSelectedTimeWindow();
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
        }
        /// <summary>
        /// Enable controls associateed with selected 
        /// type of time selection.
        /// </summary>
        void EnableSelectedTimeWindow()
        {
            //disable everything.
            dateTimePickerFrom2.Enabled = false;
            dateTimePickerFrom.Enabled = false;
            dateTimePickerTo.Enabled = false;
            numericUpDownDays.Enabled = false;

            //enable selected controls

            if (this.radioButtonFromXToY.Checked)
            {
                dateTimePickerFrom.Enabled = true;
                dateTimePickerTo.Enabled = true;
            }
            else
                if (this.radioButtonPreviousXDays.Checked)
                {
                    numericUpDownDays.Enabled = true;
                }
                else
                    if (this.radioButtonXToToday.Checked)
                    {
                        dateTimePickerFrom2.Enabled = true;

                    }
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

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.radioButtonXToToday = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.radioButtonFromXToY = new System.Windows.Forms.RadioButton();
            this.numericUpDownDays = new System.Windows.Forms.NumericUpDown();
            this.dateTimePickerFrom2 = new System.Windows.Forms.DateTimePicker();
            this.radioButtonPreviousXDays = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dateTimePickerTo);
            this.groupBox1.Controls.Add(this.radioButtonXToToday);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.dateTimePickerFrom);
            this.groupBox1.Controls.Add(this.radioButtonFromXToY);
            this.groupBox1.Controls.Add(this.numericUpDownDays);
            this.groupBox1.Controls.Add(this.dateTimePickerFrom2);
            this.groupBox1.Controls.Add(this.radioButtonPreviousXDays);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 160);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time Window Selection";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.CustomFormat = "MM/dd/yyyy hh:mm";
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTo.Location = new System.Drawing.Point(232, 64);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(120, 20);
            this.dateTimePickerTo.TabIndex = 25;
            this.dateTimePickerTo.ValueChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // radioButtonXToToday
            // 
            this.radioButtonXToToday.CausesValidation = false;
            this.radioButtonXToToday.Location = new System.Drawing.Point(24, 96);
            this.radioButtonXToToday.Name = "radioButtonXToToday";
            this.radioButtonXToToday.Size = new System.Drawing.Size(56, 24);
            this.radioButtonXToToday.TabIndex = 26;
            this.radioButtonXToToday.Text = "from";
            this.radioButtonXToToday.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(208, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 23);
            this.label12.TabIndex = 24;
            this.label12.Text = "to";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(208, 96);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 23);
            this.label11.TabIndex = 28;
            this.label11.Text = "to now.";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.CustomFormat = "MM/dd/yyyy hh:mm";
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(88, 64);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(112, 20);
            this.dateTimePickerFrom.TabIndex = 23;
            this.dateTimePickerFrom.ValueChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // radioButtonFromXToY
            // 
            this.radioButtonFromXToY.CausesValidation = false;
            this.radioButtonFromXToY.Location = new System.Drawing.Point(24, 64);
            this.radioButtonFromXToY.Name = "radioButtonFromXToY";
            this.radioButtonFromXToY.Size = new System.Drawing.Size(56, 24);
            this.radioButtonFromXToY.TabIndex = 20;
            this.radioButtonFromXToY.Text = "from";
            this.radioButtonFromXToY.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // numericUpDownDays
            // 
            this.numericUpDownDays.Location = new System.Drawing.Point(88, 26);
            this.numericUpDownDays.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownDays.Name = "numericUpDownDays";
            this.numericUpDownDays.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownDays.TabIndex = 22;
            this.numericUpDownDays.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownDays.ValueChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // dateTimePickerFrom2
            // 
            this.dateTimePickerFrom2.CustomFormat = "MM/dd/yyyy hh:mm";
            this.dateTimePickerFrom2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFrom2.Location = new System.Drawing.Point(88, 96);
            this.dateTimePickerFrom2.Name = "dateTimePickerFrom2";
            this.dateTimePickerFrom2.Size = new System.Drawing.Size(112, 20);
            this.dateTimePickerFrom2.TabIndex = 27;
            this.dateTimePickerFrom2.ValueChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // radioButtonPreviousXDays
            // 
            this.radioButtonPreviousXDays.CausesValidation = false;
            this.radioButtonPreviousXDays.Location = new System.Drawing.Point(24, 24);
            this.radioButtonPreviousXDays.Name = "radioButtonPreviousXDays";
            this.radioButtonPreviousXDays.Size = new System.Drawing.Size(72, 24);
            this.radioButtonPreviousXDays.TabIndex = 19;
            this.radioButtonPreviousXDays.Text = "previous ";
            this.radioButtonPreviousXDays.CheckedChanged += new System.EventHandler(this.SomethingChanged);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(144, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(152, 23);
            this.label13.TabIndex = 21;
            this.label13.Text = "days from now.";
            // 
            // DateSelector
            // 
            this.Controls.Add(this.groupBox1);
            this.Name = "DateSelector";
            this.Size = new System.Drawing.Size(360, 160);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion


    }
}
