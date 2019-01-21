using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using JsonFx.Serialization;
using JsonFx.Json;

namespace esp32t_monitor
{
	public partial class Form1 : Form
	{
		System.Windows.Forms.DataVisualization.Charting.Series seriesSensor1;
		System.Windows.Forms.DataVisualization.Charting.Series seriesSensor2;
		System.Windows.Forms.DataVisualization.Charting.Series seriesSensor3;
		System.Windows.Forms.DataVisualization.Charting.Series seriesSensor4;
		System.Windows.Forms.DataVisualization.Charting.Series seriesSensor5;

		List<char> arrayRxBuffer;
		bool isFirstByte;

        StreamWriter sw;


        public Form1()
		{
			InitializeComponent();

			var reader = new JsonReader();
			string dddd = @"{""t0"":20.5,""t1"":10.5}";
			var template = new { t0 = 0.0, t1 = 0.0 };
			var output = reader.Read(dddd, template);
			Console.WriteLine(output.t0);
			Console.WriteLine(output.t1);


			arrayRxBuffer = new List<char>();
			isFirstByte = false;

			this.seriesSensor1 = chart1.Series.Add("t 1");
			this.seriesSensor2 = chart1.Series.Add("t 2");
			this.seriesSensor3 = chart1.Series.Add("t 3");
			this.seriesSensor4 = chart1.Series.Add("t 4");
			this.seriesSensor5 = chart1.Series.Add("t 5");

			this.seriesSensor1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			this.seriesSensor2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			this.seriesSensor3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			this.seriesSensor4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			this.seriesSensor5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            BindingSource bs = new BindingSource();
            bs.DataSource = System.IO.Ports.SerialPort.GetPortNames();
            comboBoxListCOMport.DataSource = bs;

            //FileStream fs = new FileStream("Test.txt", FileMode.Append, FileAccess.Write);
            //fs.wr
            sw = new StreamWriter("Test.txt",true);
			Console.WriteLine("Port name is {0}", Properties.Settings.Default.PortName);
			if (Properties.Settings.Default.PortName != "") {
				//comboBoxListCOMport.Items.IndexOf("COM13")
				int idx = comboBoxListCOMport.Items.IndexOf(Properties.Settings.Default.PortName);
				if (idx >= 0) {
					comboBoxListCOMport.SelectedIndex = idx;
				}
			}
			
			
			

		}

		private void panelButton_Paint(object sender, PaintEventArgs e)
		{

		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{

            if (!serialPort1.IsOpen)
            {
				// add correct value
				Properties.Settings.Default.PortName = comboBoxListCOMport.SelectedValue.ToString();
				Properties.Settings.Default.Save();
				serialPort1.PortName = comboBoxListCOMport.SelectedValue.ToString();
                serialPort1.Open();
				buttonConnect.Text = "Disconnect";

				if (sw == null) {
                }
            }
            else {
                serialPort1.Close();
				buttonConnect.Text = "Connect";
				/*Properties.Settings.Default.PortName = comboBoxListCOMport.SelectedValue.ToString();
				Properties.Settings.Default.Save();
				serialPort1.PortName = comboBoxListCOMport.SelectedValue.ToString();
                serialPort1.Open();
				*/
            }
		}

		private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				return;
			}
			if (serialPort1.BytesToRead > 0)
			{
				char rb = (char)serialPort1.ReadByte();
				if (rb == '{') {
					arrayRxBuffer.Clear();
					arrayRxBuffer.Add(rb);
					isFirstByte = true;
				} else if (rb == '}' && isFirstByte)
				{
					arrayRxBuffer.Add(rb);
					string str_read = new string(arrayRxBuffer.ToArray());
					// Console.WriteLine(DateTime.Now.ToLongTimeString());
					//Console.WriteLine(str_read);
					

					DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(DataTemper));
					//data.ReadObject(str_read);

					var reader = new JsonReader();
					var template = new { t0 = 0.0, t1 = 0.0, t2=0.0,t3=0.0,t4=0.0};
					var output = reader.Read(str_read, template);
					

					

                    Invoke(new MethodInvoker(delegate
                    {
                        this.seriesSensor1.Points.AddY(output.t0);
                        this.seriesSensor2.Points.AddY(output.t1);
                        this.seriesSensor3.Points.AddY(output.t2);
                        this.seriesSensor4.Points.AddY(output.t3);
                        this.seriesSensor5.Points.AddY(output.t4);
                        sw.WriteLine("{5}\t\t{0}\t{1}\t{2}\t{3}\t{4}", output.t0, output.t1, output.t2, output.t3, output.t4, DateTime.Now.ToLongTimeString());

                    }));
					

					//data = JsonConvert.DeserializeObject<DataTemper>(jsReqstr);


					arrayRxBuffer.Clear();
				} else {
					arrayRxBuffer.Add(rb);
				}
	
				//countTimeStill = 0;
				if (arrayRxBuffer.Count > 500)
				{
					arrayRxBuffer.Clear();
					//countTimeStill = 100;
				}
				//Console.WriteLine(serialPort1.ReadByte());


				serialPort1_DataReceived(sender, e);
			}
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (serialPort1.IsOpen){
				serialPort1.Close();
			}
            sw.Close();
        }

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.seriesSensor1.Points.AddY(output.t0);
			this.seriesSensor2.Points.AddY(output.t1);
			this.seriesSensor3.Points.AddY(output.t2);
			this.seriesSensor4.Points.AddY(output.t3);
			this.seriesSensor5.Points.AddY(output.t4);
		}
	}


	public class DataTemper
	{
		/*
		 * 
		 * */
		public float t0 { get; set; }
		public float t1 { get; set; }
		public float t2 { get; set; }
		public float t3 { get; set; }
		public float t4 { get; set; }
	}
}
