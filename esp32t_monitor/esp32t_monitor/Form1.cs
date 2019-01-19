using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

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

		public Form1()
		{
			InitializeComponent();

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
			
		}

		private void panelButton_Paint(object sender, PaintEventArgs e)
		{

		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen) {
				serialPort1.Open();
			}
			//Console.WriteLine(re);

			//serialPort1.Close();
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
					Console.WriteLine(str_read);

					DataTemper data = JsonConvert.DeserializeObject<DataTemper>(str_read);
					Console.WriteLine(data.t0);
					Console.WriteLine(data.t1);
					Console.WriteLine(data.t2);
					Console.WriteLine(data.t3);
					Console.WriteLine(data.t4);
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
