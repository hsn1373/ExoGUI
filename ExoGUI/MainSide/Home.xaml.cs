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
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;
using System.Threading;

namespace ExoGUI.MainSide
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public static SeriesCollection SeriesCollection { get; set; }
        public static SeriesCollection SeriesCollection2 { get; set; }
        public static SeriesCollection SeriesCollection3 { get; set; }
        public static SeriesCollection SeriesCollection4 { get; set; }

        public static List<Int32> ActualPos1RecordedData = new List<Int32>();
        public static List<Int32> ActualPos2RecordedData = new List<Int32>();
        public static List<Int32> ActualPos3RecordedData = new List<Int32>();
        public static List<Int32> ActualPos4RecordedData = new List<Int32>();

        public static List<Int16> CurrentVal1RecordedData = new List<Int16>();
        public static List<Int16> CurrentVal2RecordedData = new List<Int16>();
        public static List<Int16> CurrentVal3RecordedData = new List<Int16>();
        public static List<Int16> CurrentVal4RecordedData = new List<Int16>();

        public static List<Int32> LoadCell1RecordedData = new List<Int32>();
        public static List<Int32> LoadCell2RecordedData = new List<Int32>();
        public static List<Int32> LoadCell3RecordedData = new List<Int32>();
        public static List<Int32> LoadCell4RecordedData = new List<Int32>();

        public static List<Int32> FootSensor1RecordedData = new List<Int32>();
        public static List<Int32> FootSensor2RecordedData = new List<Int32>();
        public static List<Int32> FootSensor3RecordedData = new List<Int32>();
        public static List<Int32> FootSensor4RecordedData = new List<Int32>();

        public static bool[] PositionActualRecordStatus = { false, false, false, false };
        public static bool[] CurrentActualRecordStatus = { false, false, false, false };
        public static bool[] LoadCellActualRecordStatus = { false, false, false, false };
        public static bool[] FootSensorActualRecordStatus = { false, false, false, false };
        public static bool[] PositionDesiredRecordStatus = { false, false, false, false };
        public static bool[] LoadCellDesiredRecordStatus = { false, false, false, false };
        public static bool[] EmgRightRecordStatus = { false, false, false, false };
        public static bool[] EmgLeftRecordStatus = { false, false, false, false };

        public List<string> chtimes { get; set; }

        public static int chart_counter = 0;
        public static int chart_counter2 = 1;

        private static Mutex mut = new Mutex();

        Thread left_hip_thread;
        Thread left_knee_thread;
        Thread right_hip_thread;
        Thread right_knee_thread;
        Thread record_actual_pos_thread;
        public static bool update_flag = false;
        public static bool turn_on_flag = false;
        public static bool start_record_flag = false;
        public static bool is_change_record_flag = false;

        private PLCConnection _connection;

        ~Home()
        {
            left_hip_thread.Abort();
        }

        public Home()
        {
            InitializeComponent();

            _connection = PLCConnection.getInstance("5.59.200.16.1.1", 851);
            _connection.PropertyChanged += _connection_PropertyChanged;

            chtimes = new List<string>();

            left_hip_thread = new Thread(run1);
            left_knee_thread = new Thread(run2);
            right_hip_thread = new Thread(run3);
            right_knee_thread = new Thread(run4);
            record_actual_pos_thread = new Thread(run5);

            left_hip_thread.Priority = ThreadPriority.Lowest;
            left_knee_thread.Priority = ThreadPriority.Lowest;
            right_hip_thread.Priority = ThreadPriority.Lowest;
            right_knee_thread.Priority = ThreadPriority.Lowest;

           

            left_hip_thread.Start();
            left_knee_thread.Start();
            right_hip_thread.Start();
            right_knee_thread.Start();
            record_actual_pos_thread.Start();


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
                    string tmp = ((int)_connection[X.TargetData]).ToString();
                    add_data_to_charts(tmp);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        txt_left_hip_actual.Content = tmp;
                    }));
                    update_flag = false;
                }
            }
        }
        public void run2()
        {
            while (true)
            {
                if (update_flag)
                {
                    string tmp1 = ((int)_connection[X.TargetData2]).ToString();
                    add_data_to_charts2(tmp1);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        txt_right_hip_actual.Content = tmp1;
                    }));
                    update_flag = false;
                }
            }
        }
        public void run3()
        {
            while (true)
            {
                if (update_flag)
                {
                    string tmp2 = ((int)_connection[X.TargetData3]).ToString();
                    add_data_to_charts3(tmp2);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        txt_left_knee_actual.Content = tmp2;
                    }));
                    update_flag = false;
                }
            }
        }
        public void run4()
        {
            while (true)
            {
                if (update_flag)
                {
                    string tmp3 = ((int)_connection[X.TargetData4]).ToString();
                    add_data_to_charts4(tmp3);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        txt_right_knee_actual.Content = tmp3;
                    }));
                    update_flag = false;
                }
            }
        }
        public void run5()
        {
            while (true)
            {
                if (is_change_record_flag)
                {
                    _connection[X.StartRecordFlag] = start_record_flag;
                    is_change_record_flag = false;
                }
            }
        }

        int wait_steps = 0;
        private void _connection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == X.ReadActualPos.ToString())
            {
                if (turn_on_flag)
                {
                    mut.WaitOne();
                    update_flag = true;
                    mut.ReleaseMutex();
                }
            }
            else if (e.PropertyName == X.BufferActualStatus.ToString())
            {
                //if (wait_steps < 1)
                //{
                //    wait_steps++;
                //    return;
                //}
                if (start_record_flag)
                {
                    //Application.Current.Dispatcher.Invoke(new Action(() =>
                    //{
                    try
                    {
                        var ff = (short)_connection[X.BufferActualStatus];
                    
                        //if (ff == 1)
                        //{
                            
                        //    Int32[] BufferActualPos3 = new Int32[500];
                        //    BufferActualPos3 = (Int32[])_connection[X.PositionActualValBuffer3];

                        //    Int16[] BufferCurrentVal3 = new Int16[500];
                        //    BufferCurrentVal3 = (Int16[])_connection[X.CurrentValBuffer3];

                        //    Int32[] BufferLoadCell3 = new Int32[500];
                        //    BufferLoadCell3 = (Int32[])_connection[X.LoadCellValBuffer3];

                        //    for (int i = 0; i < 500; )
                        //    {
                        //        ActualPos1RecordedData.Add(BufferActualPos3[i]);
                        //        CurrentVal1RecordedData.Add(BufferCurrentVal3[i]);
                        //        LoadCell1RecordedData.Add(BufferLoadCell3[i++]);
                                
                        //        ActualPos2RecordedData.Add(BufferActualPos3[i]);
                        //        CurrentVal2RecordedData.Add(BufferCurrentVal3[i]);
                        //        LoadCell2RecordedData.Add(BufferLoadCell3[i++]);

                        //        ActualPos3RecordedData.Add(BufferActualPos3[i]);
                        //        CurrentVal3RecordedData.Add(BufferCurrentVal3[i]);
                        //        LoadCell3RecordedData.Add(BufferLoadCell3[i++]);

                        //        ActualPos4RecordedData.Add(BufferActualPos3[i]);
                        //        CurrentVal4RecordedData.Add(BufferCurrentVal3[i]);
                        //        LoadCell4RecordedData.Add(BufferLoadCell3[i++]);
                        //    }
                        //    Console.WriteLine("MRR1");
                        //}
                        //else

                        if (ff == 2)
                        {
                            
                            Int32[] BufferActualPos1 = new Int32[500];
                            BufferActualPos1 = (Int32[])_connection[X.PositionActualValBuffer1];

                            Int16[] BufferCurrentVal1 = new Int16[500];
                            BufferCurrentVal1 = (Int16[])_connection[X.CurrentValBuffer1];

                            Int32[] BufferLoadCell1 = new Int32[500];
                            BufferLoadCell1 = (Int32[])_connection[X.LoadCellValBuffer1];

                            Int32[] BufferFootSensor1 = new Int32[500];
                            BufferFootSensor1 = (Int32[])_connection[X.FootSensorBuffer1];

                            for (int i = 0; i < 500; )
                            {
                                ActualPos1RecordedData.Add(BufferActualPos1[i]);
                                CurrentVal1RecordedData.Add(BufferCurrentVal1[i]);
                                LoadCell1RecordedData.Add(BufferLoadCell1[i]);
                                FootSensor1RecordedData.Add(BufferFootSensor1[i++]);

                                ActualPos2RecordedData.Add(BufferActualPos1[i]);
                                CurrentVal2RecordedData.Add(BufferCurrentVal1[i]);
                                LoadCell2RecordedData.Add(BufferLoadCell1[i]);
                                FootSensor2RecordedData.Add(BufferFootSensor1[i++]);

                                ActualPos3RecordedData.Add(BufferActualPos1[i]);
                                CurrentVal3RecordedData.Add(BufferCurrentVal1[i]);
                                LoadCell3RecordedData.Add(BufferLoadCell1[i]);
                                FootSensor3RecordedData.Add(BufferFootSensor1[i++]);


                                ActualPos4RecordedData.Add(BufferActualPos1[i]);
                                CurrentVal4RecordedData.Add(BufferCurrentVal1[i]);
                                LoadCell4RecordedData.Add(BufferLoadCell1[i]);
                                FootSensor4RecordedData.Add(BufferFootSensor1[i++]);
                            }
                            //Console.WriteLine("mrr0-1: " + BufferActualPos1[0].ToString());
                            //Console.WriteLine("mrr499-1: " + BufferActualPos1[499].ToString());
                            //Console.WriteLine("MRR2");
                        }
                        else if (ff == 1)
                        {
                            Int32[] BufferActualPos2 = new Int32[500];
                            BufferActualPos2 = (Int32[])_connection[X.PositionActualValBuffer2];

                            Int16[] BufferCurrentVal2 = new Int16[500];
                            BufferCurrentVal2 = (Int16[])_connection[X.CurrentValBuffer2];

                            Int32[] BufferLoadCell2 = new Int32[500];
                            BufferLoadCell2 = (Int32[])_connection[X.LoadCellValBuffer2];

                            Int32[] BufferFootSensor2 = new Int32[500];
                            BufferFootSensor2 = (Int32[])_connection[X.FootSensorBuffer2];

                            for (int i = 0; i < 500; )
                            {
                                ActualPos1RecordedData.Add(BufferActualPos2[i]);
                                CurrentVal1RecordedData.Add(BufferCurrentVal2[i]);
                                LoadCell1RecordedData.Add(BufferLoadCell2[i]);
                                FootSensor1RecordedData.Add(BufferFootSensor2[i++]);

                                ActualPos2RecordedData.Add(BufferActualPos2[i]);
                                CurrentVal2RecordedData.Add(BufferCurrentVal2[i]);
                                LoadCell2RecordedData.Add(BufferLoadCell2[i]);
                                FootSensor2RecordedData.Add(BufferFootSensor2[i++]);

                                ActualPos3RecordedData.Add(BufferActualPos2[i]);
                                CurrentVal3RecordedData.Add(BufferCurrentVal2[i]);
                                LoadCell3RecordedData.Add(BufferLoadCell2[i]);
                                FootSensor3RecordedData.Add(BufferFootSensor2[i++]);

                                ActualPos4RecordedData.Add(BufferActualPos2[i]);
                                CurrentVal4RecordedData.Add(BufferCurrentVal2[i]);
                                LoadCell4RecordedData.Add(BufferLoadCell2[i]);
                                FootSensor4RecordedData.Add(BufferFootSensor2[i++]);
                            }
                            //Console.WriteLine("mrr0-2: " + BufferActualPos2[0].ToString());
                            //Console.WriteLine("mrr499-2: " + BufferActualPos2[499].ToString());
                            //Console.WriteLine("MRR3");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //Console.WriteLine("MRR-err");
                    }
                    //}));
                }
            }
        }



        public void add_data_to_charts(string val)
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
        public void add_data_to_charts2(string val)
        {

            SeriesCollection2[0].Values.Add(Convert.ToDouble(val));
            if (SeriesCollection2[0].Values.Count > 100)
            {
                SeriesCollection2[0].Values.RemoveAt(0);
            }
        }

        public void add_data_to_charts3(string val)
        {
            SeriesCollection3[0].Values.Add(Convert.ToDouble(val));
            if (SeriesCollection3[0].Values.Count > 100)
            {
                SeriesCollection3[0].Values.RemoveAt(0);
            }
        }

        public void add_data_to_charts4(string val)
        {
            SeriesCollection4[0].Values.Add(Convert.ToDouble(val));
            if (SeriesCollection4[0].Values.Count > 100)
            {
                SeriesCollection4[0].Values.RemoveAt(0);
            }
        }

        private void btn_go_home_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["home"];
        }

        private void btn_go_desired_Click(object sender, RoutedEventArgs e)
        {
            _connection[X.Position_mode_left_hip] = float.Parse(txt_left_hip_desire.Text);
            _connection[X.Position_mode_left_knee] = float.Parse(txt_left_knee_desire.Text);
            _connection[X.Position_mode_right_hip] = float.Parse(txt_right_hip_desire.Text);
            _connection[X.Position_mode_right_knee] = float.Parse(txt_right_knee_desire.Text);
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["position_mode_point"];
        }
    }

}
