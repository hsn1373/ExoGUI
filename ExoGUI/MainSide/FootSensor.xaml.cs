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
    /// Interaction logic for FootSensor.xaml
    /// </summary>
    public partial class FootSensor : UserControl
    {
        public static SeriesCollection SeriesCollection { get; set; }
        public static SeriesCollection SeriesCollection2 { get; set; }
        public static SeriesCollection SeriesCollection3 { get; set; }
        public static SeriesCollection SeriesCollection4 { get; set; }

        public List<string> chtimes { get; set; }
        public static int chart_counter = 0;
        private PLCConnection _connection;
        public static SerialPort left_foot_sp,right_foot_sp;
        Thread left_front_thread;
        Thread left_rear_thread;
        Thread right_front_thread;
        Thread right_rear_thread;
        Thread Read_Write_From_USB;
        public static bool update_flag = false;
        public static bool read_flag = false;
        Int32 left_front_sensor, left_rear_sensor, right_front_sensor, right_rear_sensor;

        private static Mutex mut = new Mutex();
        private static Mutex mut2 = new Mutex();
        System.Diagnostics.Stopwatch watch;// = System.Diagnostics.Stopwatch.StartNew();
        int tmpTime = 0;

        public FootSensor()
        {
            InitializeComponent();
            _connection = PLCConnection.getInstance("5.59.200.16.1.1", 851);
            left_front_thread = new Thread(run1);
            left_rear_thread = new Thread(run2);
            right_front_thread = new Thread(run3);
            right_rear_thread = new Thread(run4);
            left_front_thread.Priority = ThreadPriority.Lowest;
            left_rear_thread.Priority = ThreadPriority.Lowest;
            right_front_thread.Priority = ThreadPriority.Lowest;
            right_rear_thread.Priority = ThreadPriority.Lowest;


            Read_Write_From_USB = new Thread(run5);
            Read_Write_From_USB.Priority = ThreadPriority.Highest;
            Read_Write_From_USB.Start();

            left_front_thread.Start();
            left_rear_thread.Start();
            right_front_thread.Start();
            right_rear_thread.Start();

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

        public static void open_port()
        {
            left_foot_sp = new SerialPort(BeckhoffContext.FootSensorLeft_Com_Port_Name);
            left_foot_sp.Open();
            right_foot_sp = new SerialPort(BeckhoffContext.FootSensorRight_Com_Port_Name);
            right_foot_sp.Open();
            update_flag = true;
        }


        public static void close_port()
        {
            update_flag = false;
            left_foot_sp.Close();
            right_foot_sp.Close();
        }


        public void run1()
        {
            while (true)
            {
                if (update_flag)
                {
                    try
                    {
                        add_data_to_chart1(left_front_sensor.ToString());
                        update_flag = false;
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
                        add_data_to_chart2(right_front_sensor.ToString());
                        update_flag = false;

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
                        add_data_to_chart3(left_rear_sensor.ToString());
                        update_flag = false;

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
                        add_data_to_chart4(right_rear_sensor.ToString());
                        update_flag = false;
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        Int32 left_front_sensor1 = 0, left_rear_sensor1 = 0, right_front_sensor1 = 0, right_rear_sensor1 = 0;
        public void run5()
        {
            //watch = System.Diagnostics.Stopwatch.StartNew();
            while (true)
            {
                if (read_flag)
                {
                    try
                    {
                        //*********************Read LoadCells From USB**********************************
                        string[] after_split = ReadLoadCellsFromUSB();


                        //*********************write to beckhoff*******************************
                        WriteLoadCellsToBeckhoff(left_front_sensor1, left_rear_sensor1, right_front_sensor1, right_rear_sensor1);


                        //*********************** Write LoadCells For Displaying********************************
                        WriteLoadCellForDisplaying(after_split);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("HSN:" + ex.Message);
                    }
                }
            }
        }
        public string[] ReadLoadCellsFromUSB()
        {
            string line = left_foot_sp.ReadExisting();
            line = line.Replace("\0", String.Empty);
            line = line.Replace("\r", String.Empty);
            string[] left_after_split = line.Split('\n');
            string[] one_data_left = left_after_split[1].Split(' ');
            left_front_sensor1 = Convert.ToInt32(one_data_left[0]);
            left_rear_sensor1 = Convert.ToInt32(one_data_left[1]);

            line = right_foot_sp.ReadExisting();
            line = line.Replace("\0", String.Empty);
            line = line.Replace("\r", String.Empty);
            string[] right_after_split = line.Split('\n');
            string[] one_data_right = right_after_split[1].Split(' ');
            right_front_sensor1 = Convert.ToInt32(one_data_right[0]);
            right_rear_sensor1 = Convert.ToInt32(one_data_right[1]);
            
            //***************
            string[] final = { "0", "0", "0", "0" };
            List<string> li = new List<string>();
            li.Add(one_data_left[0]);
            li.Add(one_data_left[1]);
            li.Add(one_data_right[0]);
            li.Add(one_data_right[1]);
            final = li.ToArray();
            //***************
            return final;
        }

        public void WriteLoadCellsToBeckhoff(int left_front_sensor1, int left_rear_sensor1, int right_front_sensor1, int right_rear_sensor1)
        {
            _connection[X.LeftFootFrontSensor] = left_front_sensor1;
            _connection[X.LeftFootRearSensor] = left_rear_sensor1;
            _connection[X.RightFootFrontSensor] = right_front_sensor1;
            _connection[X.RightFootRearSensor] = right_rear_sensor1;
        }

        public void WriteLoadCellForDisplaying(string[] after_split)
        {
            mut.WaitOne();
            update_flag = true;
            left_front_sensor = Convert.ToInt32(after_split[0]);
            left_rear_sensor = Convert.ToInt32(after_split[1]);
            right_front_sensor = Convert.ToInt32(after_split[2]);
            right_rear_sensor = Convert.ToInt32(after_split[3]);
            mut.ReleaseMutex();
        }

        public void add_data_to_chart1(string val)
        {
            SeriesCollection[0].Values.Add(Convert.ToDouble(val));
            chtimes.Add(chart_counter.ToString());
            chart_counter++;

            if (SeriesCollection[0].Values.Count > 30)
            {
                SeriesCollection[0].Values.RemoveAt(0);
                chtimes.RemoveAt(0);
            }
        }

        public void add_data_to_chart2(string val)
        {
            SeriesCollection2[0].Values.Add(Convert.ToDouble(val));

            if (SeriesCollection2[0].Values.Count > 30)
            {
                SeriesCollection2[0].Values.RemoveAt(0);
            }
        }

        public void add_data_to_chart3(string val)
        {
            SeriesCollection3[0].Values.Add(Convert.ToDouble(val));

            if (SeriesCollection3[0].Values.Count > 30)
            {
                SeriesCollection3[0].Values.RemoveAt(0);
            }
        }

        public void add_data_to_chart4(string val)
        {
            SeriesCollection4[0].Values.Add(Convert.ToDouble(val));

            if (SeriesCollection4[0].Values.Count > 30)
            {
                SeriesCollection4[0].Values.RemoveAt(0);
            }
        }
    }
}
