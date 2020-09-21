using System;
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
                    Title = "Actual",
                    Values = new ChartValues<double> {0} ,
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None,
                    Fill=System.Windows.Media.Brushes.Transparent
                },
                new LineSeries
                {
                    Title = "Desired",
                    Values = new ChartValues<double> {0} ,
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None,
                    Fill=System.Windows.Media.Brushes.Transparent
                }
            };
            SeriesCollection2 = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Actual",
                    Values = new ChartValues<double> {0},
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None,
                    Fill=System.Windows.Media.Brushes.Transparent
                },
                new LineSeries
                {
                    Title = "Desired",
                    Values = new ChartValues<double> {0} ,
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None,
                    Fill=System.Windows.Media.Brushes.Transparent
                }
            };
            SeriesCollection3 = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Actual",
                    Values = new ChartValues<double> {0},
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None,
                    Fill=System.Windows.Media.Brushes.Transparent
                },
                new LineSeries
                {
                    Title = "Desired",
                    Values = new ChartValues<double> {0} ,
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None,
                    Fill=System.Windows.Media.Brushes.Transparent
                }
            };
            SeriesCollection4 = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Actual",
                    Values = new ChartValues<double> {0},
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None,
                    Fill=System.Windows.Media.Brushes.Transparent
                },
                new LineSeries
                {
                    Title = "Desired",
                    Values = new ChartValues<double> {0} ,
                    PointGeometry=LiveCharts.Wpf.DefaultGeometries.None,
                    Fill=System.Windows.Media.Brushes.Transparent
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
                        add_data_to_chart1(_connection[X.LoadcellLeftHip].ToString(), _connection[X.loadCellDesiredVal1].ToString());
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
                        add_data_to_chart2(_connection[X.LoadcellRightHip].ToString(), _connection[X.loadCellDesiredVal2].ToString());

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
                        add_data_to_chart3(_connection[X.LoadcellLeftKnee].ToString(), _connection[X.loadCellDesiredVal3].ToString());

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
                        add_data_to_chart4(_connection[X.LoadcellRightknee].ToString(), _connection[X.loadCellDesiredVal4].ToString());
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void add_data_to_chart1(string val,string val2)
        {
            SeriesCollection[0].Values.Add(Convert.ToDouble(val));
            SeriesCollection[1].Values.Add(Convert.ToDouble(val2));
            chtimes.Add(chart_counter.ToString());
            chart_counter++;

            //if (SeriesCollection[0].Values.Count > 100)
            //{
            //    SeriesCollection[0].Values.RemoveAt(0);
            //    chtimes.RemoveAt(0);
            //}
            if (chart_counter == 100)
            {
                SeriesCollection[0].Values.Clear();
                SeriesCollection2[0].Values.Clear();
                SeriesCollection3[0].Values.Clear();
                SeriesCollection4[0].Values.Clear();
                SeriesCollection[1].Values.Clear();
                SeriesCollection2[1].Values.Clear();
                SeriesCollection3[1].Values.Clear();
                SeriesCollection4[1].Values.Clear();
                chart_counter = 0;
            }
        }

        public void add_data_to_chart2(string val, string val2)
        {
            SeriesCollection2[0].Values.Add(Convert.ToDouble(val));
            SeriesCollection2[1].Values.Add(Convert.ToDouble(val2));

            //if (SeriesCollection2[0].Values.Count > 100)
            //{
            //    SeriesCollection2[0].Values.RemoveAt(0);
            //    chtimes.RemoveAt(0);
            //}
        }

        public void add_data_to_chart3(string val, string val2)
        {
            SeriesCollection3[0].Values.Add(Convert.ToDouble(val));
            SeriesCollection3[1].Values.Add(Convert.ToDouble(val2));

            //if (SeriesCollection3[0].Values.Count > 100)
            //{
            //    SeriesCollection3[0].Values.RemoveAt(0);
            //}
        }

        public void add_data_to_chart4(string val, string val2)
        {
            SeriesCollection4[0].Values.Add(Convert.ToDouble(val));
            SeriesCollection4[1].Values.Add(Convert.ToDouble(val2));

            //if (SeriesCollection4[0].Values.Count > 100)
            //{
            //    SeriesCollection4[0].Values.RemoveAt(0);
            //}
        }
    }
}
