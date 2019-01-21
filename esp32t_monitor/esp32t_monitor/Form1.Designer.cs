namespace esp32t_monitor
{
	partial class Form1
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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			this.panelButton = new System.Windows.Forms.Panel();
			this.comboBoxListCOMport = new System.Windows.Forms.ComboBox();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.panelChart = new System.Windows.Forms.Panel();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
			this.checkedListBoxSeriesView = new System.Windows.Forms.CheckedListBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panelButton.SuspendLayout();
			this.panelChart.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			this.SuspendLayout();
			// 
			// panelButton
			// 
			this.panelButton.Controls.Add(this.checkedListBoxSeriesView);
			this.panelButton.Controls.Add(this.comboBoxListCOMport);
			this.panelButton.Controls.Add(this.buttonConnect);
			this.panelButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelButton.Location = new System.Drawing.Point(0, 0);
			this.panelButton.Name = "panelButton";
			this.panelButton.Size = new System.Drawing.Size(800, 93);
			this.panelButton.TabIndex = 0;
			this.panelButton.Paint += new System.Windows.Forms.PaintEventHandler(this.panelButton_Paint);
			// 
			// comboBoxListCOMport
			// 
			this.comboBoxListCOMport.FormattingEnabled = true;
			this.comboBoxListCOMport.Location = new System.Drawing.Point(12, 22);
			this.comboBoxListCOMport.Name = "comboBoxListCOMport";
			this.comboBoxListCOMport.Size = new System.Drawing.Size(121, 21);
			this.comboBoxListCOMport.TabIndex = 1;
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(150, 20);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(75, 23);
			this.buttonConnect.TabIndex = 0;
			this.buttonConnect.Text = "Connect";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// panelChart
			// 
			this.panelChart.Controls.Add(this.chart1);
			this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelChart.Location = new System.Drawing.Point(0, 93);
			this.panelChart.Name = "panelChart";
			this.panelChart.Size = new System.Drawing.Size(800, 357);
			this.panelChart.TabIndex = 1;
			// 
			// chart1
			// 
			chartArea3.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea3);
			this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
			legend3.Name = "Legend1";
			this.chart1.Legends.Add(legend3);
			this.chart1.Location = new System.Drawing.Point(0, 0);
			this.chart1.Name = "chart1";
			this.chart1.Size = new System.Drawing.Size(800, 357);
			this.chart1.TabIndex = 0;
			this.chart1.Text = "chart1";
			// 
			// serialPort1
			// 
			this.serialPort1.BaudRate = 115200;
			this.serialPort1.PortName = "COM8";
			this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
			// 
			// checkedListBoxSeriesView
			// 
			this.checkedListBoxSeriesView.FormattingEnabled = true;
			this.checkedListBoxSeriesView.Items.AddRange(new object[] {
            "t1",
            "t2",
            "t3",
            "t4",
            "t5"});
			this.checkedListBoxSeriesView.Location = new System.Drawing.Point(728, 8);
			this.checkedListBoxSeriesView.Name = "checkedListBoxSeriesView";
			this.checkedListBoxSeriesView.Size = new System.Drawing.Size(60, 79);
			this.checkedListBoxSeriesView.TabIndex = 2;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panelChart);
			this.Controls.Add(this.panelButton);
			this.Name = "Form1";
			this.Text = "ESP32 with MAX6675 sensors monitor";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.panelButton.ResumeLayout(false);
			this.panelChart.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelButton;
		private System.Windows.Forms.Panel panelChart;
		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private System.IO.Ports.SerialPort serialPort1;
		private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ComboBox comboBoxListCOMport;
		private System.Windows.Forms.CheckedListBox checkedListBoxSeriesView;
		private System.Windows.Forms.Timer timer1;
	}
}

