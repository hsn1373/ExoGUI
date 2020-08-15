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
        public static SerialPort sp;
        Thread left_hip_thread;
        Thread left_knee_thread;
        Thread right_hip_thread;
        Thread right_knee_thread;
        Thread Read_Write_From_USB;
        public static bool update_flag = false;
        public static bool read_flag = false;
        Int32 loadcell_left_hip, loadcell_left_knee, loadcell_right_hip, loadcell_right_knee;

        private static Mutex mut = new Mutex();
        private static Mutex mut2 = new Mutex();
        System.Diagnostics.Stopwatch watch;// = System.Diagnostics.Stopwatch.StartNew();
        int tmpTime = 0;

        public LoadCell()
        {
            InitializeComponent();
            _connection = PLCConnection.getInstance("5.59.200.16.1.1", 851);
            left_hip_thread = new Thread(run1);
            left_knee_thread = new Thread(run2);
            right_hip_thread = new Thread(run3);
            right_knee_thread = new Thread(run4);
            left_hip_thread.Priority = ThreadPriority.Lowest;
            left_knee_thread.Priority = ThreadPriority.Lowest;
            right_hip_thread.Priority = ThreadPriority.Lowest;
            right_knee_thread.Priority = ThreadPriority.Lowest;


            Read_Write_From_USB = new Thread(run5);
            Read_Write_From_USB.Priority = ThreadPriority.Highest;
            Read_Write_From_USB.Start();

            right_knee_thread.Start();
            right_hip_thread.Start();
            left_knee_thread.Start();
            left_hip_thread.Start();

            chtimes = new List<string>();

            //sp = new SerialPort("COM11");
            //sp.Open();

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
            sp = new SerialPort(BeckhoffContext.LoadCell_Com_Port_Name);
            sp.Open();
            update_flag = true;
        }


        public static void close_port()
        {
            update_flag = false;
            sp.Close();
        }


        public void run1()
        {
            while (true)
            {
                if (update_flag)
                {
                    try
                    {
                        add_data_to_chart1(loadcell_left_hip.ToString());
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
                        add_data_to_chart2(loadcell_right_hip.ToString());
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
                        add_data_to_chart3(loadcell_left_knee.ToString());
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
                        add_data_to_chart4(loadcell_right_knee.ToString());
                        update_flag = false;
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        Int32 loadcell_left_hip1 = 0, loadcell_left_knee1 = 0, loadcell_right_hip1 = 0, loadcell_right_knee1 = 0;
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


                        //*********************Display to output for testing (temporary)**********************************
                        //Console.WriteLine(loadcell_left_hip1 + "**" + loadcell_left_knee1 + "**" + loadcell_right_hip1 + "**" + loadcell_right_knee1);
                        //Thread.Sleep(100);


                        //*********************RecordedData**********************************
                        //RecordData();

                        //*********************write to beckhoff*******************************
                        WriteLoadCellsToBeckhoff(loadcell_left_hip1, loadcell_left_knee1, loadcell_right_hip1, loadcell_right_knee1);



                        //_connection[X.PreTimeFromPC] = tmpTime;
                        //tmpTime = Convert.ToInt32(watch.ElapsedMilliseconds);
                        //_connection[X.TimeFromPC] = tmpTime;
                        //*******************************************************



                        //*********************** Write LoadCells For Displaying********************************
                        WriteLoadCellForDisplaying(after_split);
                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("MRR:" + ex.Message);
                        //Console.WriteLine("mrr");
                    }
                }
            }
        }
        public string[] ReadLoadCellsFromUSB()
        {
            string line = sp.ReadExisting();
            line = line.Replace("\0", String.Empty);
            line = line.Replace("\r", String.Empty);
            line = line.Replace("\n", " ");
            string[] after_split = line.Split(' ');
            if (after_split.Length >= 5)
            {
                loadcell_left_hip1 = Convert.ToInt32(after_split[after_split.Length - 5]);
                loadcell_left_knee1 = Convert.ToInt32(after_split[after_split.Length - 4]);
                loadcell_right_knee1 = Convert.ToInt32(after_split[after_split.Length - 3]);
                loadcell_right_hip1 = Convert.ToInt32(after_split[after_split.Length - 2]);
            }
            return after_split;
        }
        public void WriteLoadCellsToBeckhoff(int _loadcell_left_hip1, int _loadcell_left_knee1, int _loadcell_right_hip1, int _loadcell_right_knee1)
        {
            _connection[X.LoadcellLeftHip] = _loadcell_left_hip1;
            _connection[X.LoadcellLeftKnee] = _loadcell_left_knee1;
            _connection[X.LoadcellRightHip] = _loadcell_right_hip1;
            _connection[X.LoadcellRightknee] = _loadcell_right_knee1;
        }
        public void WriteLoadCellForDisplaying(string[] after_split)
        {
            mut.WaitOne();
            update_flag = true;
            loadcell_left_hip = Convert.ToInt32(after_split[after_split.Length - 5]);
            loadcell_left_knee = Convert.ToInt32(after_split[after_split.Length - 4]);
            loadcell_right_knee = Convert.ToInt32(after_split[after_split.Length - 3]);
            loadcell_right_hip = Convert.ToInt32(after_split[after_split.Length - 2]);
            mut.ReleaseMutex();
        }
        public void RecordData()
        {
            mut2.WaitOne();
            if (Home.start_record_flag)
            {
                LoadCell1RecordedData.Add(loadcell_left_hip);
                LoadCell2RecordedData.Add(loadcell_left_knee);
                LoadCell3RecordedData.Add(loadcell_right_hip);
                LoadCell4RecordedData.Add(loadcell_right_knee);
            }
            mut2.ReleaseMutex();
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
