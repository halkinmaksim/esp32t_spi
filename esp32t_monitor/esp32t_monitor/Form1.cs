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



		Random rand;

		List<char> arrayRxBuffer;
		bool isFirstByte;

        StreamWriter sw;


        public Form1()
		{
			InitializeComponent();

			rand = new Random();

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
			//this.seriesSensor1.Color = 
			this.seriesSensor2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			this.seriesSensor3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			this.seriesSensor4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			this.seriesSensor5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

			for(int i = 0; i < checkedListBoxSeriesView.Items.Count; i++) {
				checkedListBoxSeriesView.SetItemChecked(i, true);
			}

			textBox1.Text = Properties.Settings.Default.A1.ToString();
			textBox2.Text = Properties.Settings.Default.B1.ToString();
			textBox3.Text = Properties.Settings.Default.A2.ToString();
			textBox4.Text = Properties.Settings.Default.B2.ToString();
			textBox5.Text = Properties.Settings.Default.A3.ToString();
			textBox6.Text = Properties.Settings.Default.B3.ToString();
			textBox7.Text = Properties.Settings.Default.A4.ToString();
			textBox8.Text = Properties.Settings.Default.B4.ToString();
			textBox9.Text = Properties.Settings.Default.A5.ToString();
			textBox10.Text = Properties.Settings.Default.B5.ToString();


			//chart1.ChartAreas[0].AxisX.ScaleView.Zoom(2, 3);
			// Enable range selection and zooming end user interface
			//chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
			chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
			//chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
			chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

			chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
			chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

			
			//chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;



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
                        this.seriesSensor1.Points.AddY(output.t0 * Properties.Settings.Default.A1 + Properties.Settings.Default.B1);
                        this.seriesSensor2.Points.AddY(output.t1 * Properties.Settings.Default.A2 + Properties.Settings.Default.B2);
                        this.seriesSensor3.Points.AddY(output.t2 * Properties.Settings.Default.A3 + Properties.Settings.Default.B3);
                        this.seriesSensor4.Points.AddY(output.t3 * Properties.Settings.Default.A4 + Properties.Settings.Default.B4);
                        this.seriesSensor5.Points.AddY(output.t4 * Properties.Settings.Default.A5 + Properties.Settings.Default.B5);
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
			this.seriesSensor1.Points.AddY(rand.Next(100) * Properties.Settings.Default.A1 + Properties.Settings.Default.B1);
			this.seriesSensor2.Points.AddY(rand.Next(100) * Properties.Settings.Default.A2 + Properties.Settings.Default.B2);
			this.seriesSensor3.Points.AddY(rand.Next(100) * Properties.Settings.Default.A3 + Properties.Settings.Default.B3);
			this.seriesSensor4.Points.AddY(rand.Next(100) * Properties.Settings.Default.A4 + Properties.Settings.Default.B4);
			this.seriesSensor5.Points.AddY(rand.Next(100) * Properties.Settings.Default.A5 + Properties.Settings.Default.B5);
		}

		private void checkedListBoxSeriesView_SelectedIndexChanged(object sender, EventArgs e)
		{
			CheckedListBox.CheckedItemCollection curItem = checkedListBoxSeriesView.CheckedItems;

			for (int i = 0; i < checkedListBoxSeriesView.Items.Count; i++) {
				if (checkedListBoxSeriesView.GetItemChecked(i)) {
					chart1.Series[i].Enabled = true;
					Console.WriteLine(checkedListBoxSeriesView.GetItemText(i));
					
				}
				else {
					chart1.Series[i].Enabled = false;
				}
			}
			
		}

		private void chart1_DoubleClick(object sender, EventArgs e)
		{
			chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
			chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
		}

		private void checkedListBoxSeriesView_SelectedValueChanged(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			float result;
			if (String.IsNullOrEmpty(textBox1.Text) || !float.TryParse(textBox1.Text, out result))
			{
				textBox1.Text = Properties.Settings.Default.A1.ToString();
			}
			else {
				Properties.Settings.Default.A1 = result;
				Properties.Settings.Default.Save();
			}
			
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			float result;
			if (String.IsNullOrEmpty(textBox2.Text) || !float.TryParse(textBox2.Text, out result))
			{
				textBox2.Text = Properties.Settings.Default.B1.ToString();
			}
			else
			{
				Properties.Settings.Default.B1 = result;
				Properties.Settings.Default.Save();
			}
		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{
			float result;
			if (String.IsNullOrEmpty(textBox3.Text) || !float.TryParse(textBox3.Text, out result))
			{
				textBox3.Text = Properties.Settings.Default.A2.ToString();
			}
			else
			{
				Properties.Settings.Default.A2 = result;
				Properties.Settings.Default.Save();
			}
		}

		private void textBox4_TextChanged(object sender, EventArgs e)
		{
			float result;
			if (String.IsNullOrEmpty(textBox4.Text) || !float.TryParse(textBox4.Text, out result))
			{
				textBox4.Text = Properties.Settings.Default.B2.ToString();
			}
			else
			{
				Properties.Settings.Default.B2 = result;
				Properties.Settings.Default.Save();
			}
		}

		private void textBox5_TextChanged(object sender, EventArgs e)
		{
			float result;
			if (String.IsNullOrEmpty(textBox5.Text) || !float.TryParse(textBox5.Text, out result))
			{
				textBox5.Text = Properties.Settings.Default.A3.ToString();
			}
			else
			{
				Properties.Settings.Default.A3 = result;
				Properties.Settings.Default.Save();
			}
		}

		private void textBox6_TextChanged(object sender, EventArgs e)
		{
			float result;
			if (String.IsNullOrEmpty(textBox6.Text) || !float.TryParse(textBox6.Text, out result))
			{
				textBox6.Text = Properties.Settings.Default.B3.ToString();
			}
			else
			{
				Properties.Settings.Default.B3 = result;
				Properties.Settings.Default.Save();
			}
		}

		private void textBox7_TextChanged(object sender, EventArgs e)
		{
			float result;
			if (String.IsNullOrEmpty(textBox7.Text) || !float.TryParse(textBox7.Text, out result))
			{
				textBox7.Text = Properties.Settings.Default.A4.ToString();
			}
			else
			{
				Properties.Settings.Default.A4 = result;
				Properties.Settings.Default.Save();
			}
		}
		private void textBox8_TextChanged(object sender, EventArgs e)
		{
			float result;
			if (String.IsNullOrEmpty(textBox8.Text) || !float.TryParse(textBox8.Text, out result))
			{
				textBox8.Text = Properties.Settings.Default.B4.ToString();
			}
			else
			{
				Properties.Settings.Default.B4 = result;
				Properties.Settings.Default.Save();
			}
		}
		private void textBox9_TextChanged(object sender, EventArgs e)
		{
			float result;
			if (String.IsNullOrEmpty(textBox9.Text) || !float.TryParse(textBox9.Text, out result))
			{
				textBox9.Text = Properties.Settings.Default.A5.ToString();
			}
			else
			{
				Properties.Settings.Default.A5 = result;
				Properties.Settings.Default.Save();
			}
		}
		private void textBox10_TextChanged(object sender, EventArgs e)
		{
			float result;
			if (String.IsNullOrEmpty(textBox10.Text) || !float.TryParse(textBox10.Text, out result))
			{
				textBox10.Text = Properties.Settings.Default.B5.ToString();
			}
			else
			{
				Properties.Settings.Default.B5 = result;
				Properties.Settings.Default.Save();
			}
		}

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                float t0, t1, t2, t3, t4;
                StreamWriter save_file_w = new StreamWriter(saveFileDialog1.FileName);
                int cnt = chart1.Series[0].Points.Count();
                for (int i = 0; i < cnt; i++) {
                    t0 = (float)this.seriesSensor1.Points[i].YValues[0];
                    t1 = (float)this.seriesSensor2.Points[i].YValues[0];
                    t2 = (float)this.seriesSensor3.Points[i].YValues[0];
                    t3 = (float)this.seriesSensor4.Points[i].YValues[0];
                    t4 = (float)this.seriesSensor5.Points[i].YValues[0];
                    save_file_w.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", t0, t1, t2, t3, t4, DateTime.Now.ToLongTimeString());
                }

                
                save_file_w.Close();
            }
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
