﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExoGUI.NetWork;
using System.IO.Ports;
using System.Threading;
using LiveCharts;
using LiveCharts.Wpf;

namespace ExoGUI.MainSide
{
    /// <summary>
    /// Interaction logic for LoadCell.xaml
    /// </summary>
    public partial class LoadCell : UserControl
    {


        public static SeriesCollection SeriesCollection { get; set; }
        public static SeriesCollection SeriesCollection2 { get; set; }
        public static SeriesCollection SeriesCollection3 { get; set; }
        public static SeriesCollection SeriesCollection4 { get; set; }

        List<Int32> LoadCell1RecordedData = new List<Int32>();
        List<Int32> LoadCell2RecordedData = new List<Int32>();
        List<Int32> LoadCell3RecordedData = new List<Int32>();
        List<Int32> LoadCell4RecordedData = new List<Int32>();

        public List<string> chtimes { get; set; }
        public static int chart_counter = 0;
        private PLCConnection _connection;
        Thread left_hip_thread;
        Thread left_knee_thread;
        Thread right_hip_thread;
        Thread right_knee_thread;
        public static bool update_flag = false;
        public static bool read_flag = false;

        public LoadCell()
        {
            InitializeComponent();
            _connection = PLCConnection.getInstance("5.59.200.16.1.1", 851);
            left_hip_thread = new Thread(run1);
            left_knee_thread = new Thread(run2);
            right_hip_thread = new Thread(run3);
            right_knee_thread = new Thread(run4);

            right_knee_thread.Start();
            right_hip_thread.Start();
            left_knee_thread.Start();
            left_hip_thread.Start();

            chtimes = new List<string>();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> {0} ,
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None
                }
            };
            SeriesCollection2 = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> {0},
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None
                }
            };
            SeriesCollection3 = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> {0},
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None
                }
            };
            SeriesCollection4 = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> {0},
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None
                }
            };
            DataContext = this;
        }

        public void run1()
        {
            while (true)
            {
                if (update_flag)
                {
                    try
                    {
                        add_data_to_chart1(_connection[X.LoadcellLeftHip].ToString());
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void run2()
        {
            while (true)
            {
                if (update_flag)
                {
                    try
                    {
                        add_data_to_chart2(_connection[X.LoadcellRightHip].ToString());

                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void run3()
        {
            while (true)
            {
                if (update_flag)
                {
                    try
                    {
                        add_data_to_chart3(_connection[X.LoadcellLeftKnee].ToString());

                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void run4()
        {
            while (true)
            {
                if (update_flag)
                {
                    try
                    {
                        add_data_to_chart4(_connection[X.LoadcellRightknee].ToString());
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void add_data_to_chart1(string val)
        {
            SeriesCollection[0].Values.Add(Convert.ToDouble(val));
            chtimes.Add(chart_counter.ToString());
            chart_counter++;

            if (SeriesCollection[0].Values.Count > 100)
            {
                SeriesCollection[0].Values.RemoveAt(0);
                chtimes.RemoveAt(0);
            }
        }

        public void add_data_to_chart2(string val)
        {
            SeriesCollection2[0].Values.Add(Convert.ToDouble(val));

            if (SeriesCollection2[0].Values.Count > 100)
            {
                SeriesCollection2[0].Values.RemoveAt(0);
            }
        }

        public void add_data_to_chart3(string val)
        {
            SeriesCollection3[0].Values.Add(Convert.ToDouble(val));

            if (SeriesCollection3[0].Values.Count > 100)
            {
                SeriesCollection3[0].Values.RemoveAt(0);
            }
        }

        public void add_data_to_chart4(string val)
        {
            SeriesCollection4[0].Values.Add(Convert.ToDouble(val));

            if (SeriesCollection4[0].Values.Count > 100)
            {
                SeriesCollection4[0].Values.RemoveAt(0);
            }
        }
    }
}
