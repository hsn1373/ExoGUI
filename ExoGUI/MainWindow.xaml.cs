using ExoGUI.NetWork;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using ExoGUI.MainSide;
using System.Threading;
using Microsoft.Office.Interop;
using System.Runtime.InteropServices;

namespace ExoGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private static Mutex mut = new Mutex();
        int popup_record_setting_number = 0;

        public MainWindow()
        {
            InitializeComponent();
            BeckhoffContext BK = new BeckhoffContext("5.59.200.16.1.1", 851);
            BK.StartController();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }



        private void btn_go_home_Click(object sender, RoutedEventArgs e)
        {
            btn_go_home.Background = (System.Windows.Media.Brush)Application.Current.Resources["ActiveButton"];
            btn_load_cell.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_foot_sensor.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_trajectory.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_settings.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];

            grd_home.Visibility = System.Windows.Visibility.Visible;
            grd_loadcell.Visibility = System.Windows.Visibility.Hidden;
            grd_footsensor.Visibility = System.Windows.Visibility.Hidden;
            grd_trajectory.Visibility = System.Windows.Visibility.Hidden;
            grd_settings.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btn_load_cell_Click(object sender, RoutedEventArgs e)
        {
            btn_go_home.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_load_cell.Background = (System.Windows.Media.Brush)Application.Current.Resources["ActiveButton"];
            btn_foot_sensor.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_trajectory.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_settings.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];

            grd_home.Visibility = System.Windows.Visibility.Hidden;
            grd_loadcell.Visibility = System.Windows.Visibility.Visible;
            grd_footsensor.Visibility = System.Windows.Visibility.Hidden;
            grd_trajectory.Visibility = System.Windows.Visibility.Hidden;
            grd_settings.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btn_foot_sensor_Click(object sender, RoutedEventArgs e)
        {
            btn_go_home.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_load_cell.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_foot_sensor.Background = (System.Windows.Media.Brush)Application.Current.Resources["ActiveButton"];
            btn_trajectory.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_settings.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];

            grd_home.Visibility = System.Windows.Visibility.Hidden;
            grd_loadcell.Visibility = System.Windows.Visibility.Hidden;
            grd_footsensor.Visibility = System.Windows.Visibility.Visible;
            grd_trajectory.Visibility = System.Windows.Visibility.Hidden;
            grd_settings.Visibility = System.Windows.Visibility.Hidden;
        }
        private void btn_trajectory_Click(object sender, RoutedEventArgs e)
        {
            btn_go_home.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_load_cell.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_foot_sensor.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_trajectory.Background = (System.Windows.Media.Brush)Application.Current.Resources["ActiveButton"];
            btn_settings.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];

            grd_home.Visibility = System.Windows.Visibility.Hidden;
            grd_loadcell.Visibility = System.Windows.Visibility.Hidden;
            grd_footsensor.Visibility = System.Windows.Visibility.Hidden;
            grd_trajectory.Visibility = System.Windows.Visibility.Visible;
            grd_settings.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btn_settings_Click(object sender, RoutedEventArgs e)
        {
            btn_go_home.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_load_cell.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_foot_sensor.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_trajectory.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            btn_settings.Background = (System.Windows.Media.Brush)Application.Current.Resources["ActiveButton"];

            grd_home.Visibility = System.Windows.Visibility.Hidden;
            grd_loadcell.Visibility = System.Windows.Visibility.Hidden;
            grd_footsensor.Visibility = System.Windows.Visibility.Hidden;
            grd_trajectory.Visibility = System.Windows.Visibility.Hidden;
            grd_settings.Visibility = System.Windows.Visibility.Visible;
        }

        private void btn_run_Click(object sender, RoutedEventArgs e)
        {

            if(btn_run.Content.ToString() == "SETUP DRIVERS")
            {
                btn_run.Content = "EXECUTE";
                btn_run.Background = (System.Windows.Media.Brush)Application.Current.Resources["WarningBrush"];
                BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["run"];
            }
            else if(btn_run.Content.ToString() == "EXECUTE")
            {
                BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys[BeckhoffContext.Assist_Algorithm];
                btn_run.Background = (System.Windows.Media.Brush)Application.Current.Resources["SuccessBrush"];
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["clear_error"];
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            btn_run.Content = "SETUP DRIVERS";
            btn_run.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["stop"];

            Home.turn_on_flag = false;
            LoadCell.update_flag = false;
            FootSensor.update_flag = false;
            //btn_record.IsEnabled = false;
            //btn_run.IsEnabled = false;
            //btn_clear.IsEnabled = false;
            //btn_stop.IsEnabled = false;
        }

        private void btn_record_Click(object sender, RoutedEventArgs e)
        {
            if (btn_record.Content.ToString() == "RECORD")
            {
                Home.ActualPos1RecordedData.Clear();
                Home.ActualPos2RecordedData.Clear();
                Home.ActualPos3RecordedData.Clear();
                Home.ActualPos4RecordedData.Clear();
                Home.CurrentVal1RecordedData.Clear();
                Home.CurrentVal2RecordedData.Clear();
                Home.CurrentVal3RecordedData.Clear();
                Home.CurrentVal4RecordedData.Clear();
                Home.LoadCell1RecordedData.Clear();
                Home.LoadCell2RecordedData.Clear();
                Home.LoadCell3RecordedData.Clear();
                Home.LoadCell4RecordedData.Clear();
                Home.FootSensor1RecordedData.Clear();
                Home.FootSensor2RecordedData.Clear();
                Home.FootSensor3RecordedData.Clear();
                Home.FootSensor4RecordedData.Clear();
                Home.PositionDesired1RecordedData.Clear();
                Home.PositionDesired2RecordedData.Clear();
                Home.PositionDesired3RecordedData.Clear();
                Home.PositionDesired4RecordedData.Clear();
                Home.LoadCellDesired1RecordedData.Clear();
                Home.LoadCellDesired2RecordedData.Clear();
                Home.LoadCellDesired3RecordedData.Clear();
                Home.LoadCellDesired4RecordedData.Clear();
                Home.EMGRight1RecordedData.Clear();
                Home.EMGRight2RecordedData.Clear();
                Home.EMGRight3RecordedData.Clear();
                Home.EMGRight4RecordedData.Clear();
                Home.EMGLeft1RecordedData.Clear();
                Home.EMGLeft2RecordedData.Clear();
                Home.EMGLeft3RecordedData.Clear();
                Home.EMGLeft4RecordedData.Clear();
                Record_popup.IsOpen = true;
            }
            else
            {
                Home.start_record_flag = false;
                //for (int i = 0; i < Home.ActualPos1RecordedData.Count; i++)
                //    Console.WriteLine(Home.ActualPos1RecordedData[i]);
                Home.is_change_record_flag = true;
                btn_record.Content = "RECORD";
                btn_record.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    WriteToExcel(@"D:\\Data.csv", Home.ActualPos1RecordedData, Home.ActualPos2RecordedData, Home.ActualPos3RecordedData, Home.ActualPos4RecordedData,
                    Home.CurrentVal1RecordedData, Home.CurrentVal2RecordedData, Home.CurrentVal3RecordedData, Home.CurrentVal4RecordedData,
                    Home.LoadCell1RecordedData, Home.LoadCell2RecordedData, Home.LoadCell3RecordedData, Home.LoadCell4RecordedData,
                    Home.FootSensor1RecordedData, Home.FootSensor2RecordedData, Home.FootSensor3RecordedData, Home.FootSensor4RecordedData,
                    Home.PositionDesired1RecordedData, Home.PositionDesired2RecordedData, Home.PositionDesired3RecordedData, Home.PositionDesired4RecordedData,
                    Home.LoadCellDesired1RecordedData, Home.LoadCellDesired2RecordedData, Home.LoadCellDesired3RecordedData, Home.LoadCellDesired4RecordedData,
                    Home.EMGRight1RecordedData, Home.EMGRight2RecordedData, Home.EMGRight3RecordedData, Home.EMGRight4RecordedData,
                    Home.EMGLeft1RecordedData, Home.EMGLeft2RecordedData, Home.EMGLeft3RecordedData, Home.EMGLeft4RecordedData);
                }));
            }

        }
        public void WriteToExcel(string Addres, List<Int32> _Apos1, List<Int32> _Apos2, List<Int32> _Apos3, List<Int32> _Apos4,
           List<Int32> _CurrentVal1, List<Int32> _CurrentVal2, List<Int32> _CurrentVal3, List<Int32> _CurrentVal4,
           List<Int32> _LoadCell1, List<Int32> _LoadCell2, List<Int32> _LoadCell3, List<Int32> _LoadCell4,
           List<Int32> _FootSensor1, List<Int32> _FootSensor2, List<Int32> _FootSensor3, List<Int32> _FootSensor4,
           List<Int32> _DesiredPos1, List<Int32> _DesiredPos2, List<Int32> _DesiredPos3, List<Int32> _DesiredPos4,
           List<Int32> _DesiredLoadCell1, List<Int32> _DesiredLoadCell2, List<Int32> _DesiredLoadCell3, List<Int32> _DesiredLoadCell4,
           List<Int32> _EmgRight1, List<Int32> _EmgRight2, List<Int32> _EmgRight3, List<Int32> _EmgRight4,
           List<Int32> _EmgLeft1, List<Int32> _EmgLeft2, List<Int32> _EmgLeft3, List<Int32> _EmgLeft4)
        {
            try
            {
                
                string strSeperator = ",";
                StringBuilder sbOutput = new StringBuilder();

                int min = Math.Min(_Apos1.Count, _CurrentVal1.Count);
                int min2 = Math.Min(min, _LoadCell1.Count);
                int min3 = Math.Min(min2, _FootSensor1.Count);

                for (int i = 0; i < min3; i++)
                    sbOutput.AppendLine(string.Join(strSeperator,
                        _Apos1[i].ToString(),
                        _Apos2[i].ToString(),
                        _Apos3[i].ToString(),
                        _Apos4[i].ToString(),
                        _CurrentVal1[i].ToString(),
                        _CurrentVal2[i].ToString(),
                        _CurrentVal3[i].ToString(),
                        _CurrentVal4[i].ToString(),
                        _LoadCell1[i].ToString(),
                        _LoadCell2[i].ToString(),
                        _LoadCell3[i].ToString(),
                        _LoadCell4[i].ToString(),
                        _FootSensor1[i].ToString(),
                        _FootSensor2[i].ToString(),
                        _FootSensor3[i].ToString(),
                        _FootSensor4[i].ToString(),
                        _DesiredPos1[i].ToString(),
                        _DesiredPos2[i].ToString(),
                        _DesiredPos3[i].ToString(),
                        _DesiredPos4[i].ToString(),
                        _DesiredLoadCell1[i].ToString(),
                        _DesiredLoadCell2[i].ToString(),
                        _DesiredLoadCell3[i].ToString(),
                        _DesiredLoadCell4[i].ToString(),
                        _EmgRight1[i].ToString(),
                        _EmgRight2[i].ToString(),
                        _EmgRight3[i].ToString(),
                        _EmgRight4[i].ToString(),
                        _EmgLeft1[i].ToString(),
                        _EmgLeft2[i].ToString(),
                        _EmgLeft3[i].ToString(),
                        _EmgLeft4[i].ToString()
                        ));

                // Create and write the csv file
                File.WriteAllText(Addres, sbOutput.ToString());

                MessageBox.Show("CSV file created , you can find the file" + Addres);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void btn_popup_position_actual_value_Click(object sender, RoutedEventArgs e)
        {
            popup_chkbox_first.Content = "Position Actual Value 1";
            popup_chkbox_second.Content = "Position Actual Value 2";
            popup_chkbox_third.Content = "Position Actual Value 3";
            popup_chkbox_forth.Content = "Position Actual Value 4";
            popup_chkbox_first.IsChecked = Home.PositionActualRecordStatus[0];
            popup_chkbox_second.IsChecked = Home.PositionActualRecordStatus[1];
            popup_chkbox_third.IsChecked = Home.PositionActualRecordStatus[2];
            popup_chkbox_forth.IsChecked = Home.PositionActualRecordStatus[3];
            popup_record_setting_number = 0;
        }

        private void btn_popup_current_actual_value_Click(object sender, RoutedEventArgs e)
        {
            popup_chkbox_first.Content = "Current Actual Value 1";
            popup_chkbox_second.Content = "Current Actual Value 2";
            popup_chkbox_third.Content = "Current Actual Value 3";
            popup_chkbox_forth.Content = "Current Actual Value 4";
            popup_chkbox_first.IsChecked = Home.CurrentActualRecordStatus[0];
            popup_chkbox_second.IsChecked = Home.CurrentActualRecordStatus[1];
            popup_chkbox_third.IsChecked = Home.CurrentActualRecordStatus[2];
            popup_chkbox_forth.IsChecked = Home.CurrentActualRecordStatus[3];
            popup_record_setting_number = 1;
        }

        private void btn_popup_loadcell_actual_value_Click(object sender, RoutedEventArgs e)
        {
            popup_chkbox_first.Content = "LoadCell Actual Value 1";
            popup_chkbox_second.Content = "LoadCell Actual Value 2";
            popup_chkbox_third.Content = "LoadCell Actual Value 3";
            popup_chkbox_forth.Content = "LoadCell Actual Value 4";
            popup_chkbox_first.IsChecked = Home.LoadCellActualRecordStatus[0];
            popup_chkbox_second.IsChecked = Home.LoadCellActualRecordStatus[1];
            popup_chkbox_third.IsChecked = Home.LoadCellActualRecordStatus[2];
            popup_chkbox_forth.IsChecked = Home.LoadCellActualRecordStatus[3];
            popup_record_setting_number = 2;
        }

        private void btn_popup_footsensor_actual_value_Click(object sender, RoutedEventArgs e)
        {
            popup_chkbox_first.Content = "FootSensor Actual Value 1";
            popup_chkbox_second.Content = "FootSensor Actual Value 2";
            popup_chkbox_third.Content = "FootSensor Actual Value 3";
            popup_chkbox_forth.Content = "FootSensor Actual Value 4";
            popup_chkbox_first.IsChecked = Home.FootSensorActualRecordStatus[0];
            popup_chkbox_second.IsChecked = Home.FootSensorActualRecordStatus[1];
            popup_chkbox_third.IsChecked = Home.FootSensorActualRecordStatus[2];
            popup_chkbox_forth.IsChecked = Home.FootSensorActualRecordStatus[3];
            popup_record_setting_number = 3;
        }

        private void btn_popup_position_desire_value_Click(object sender, RoutedEventArgs e)
        {
            popup_chkbox_first.Content = "Position Desired Value 1";
            popup_chkbox_second.Content = "Position Desired Value 2";
            popup_chkbox_third.Content = "Position Desired Value 3";
            popup_chkbox_forth.Content = "Position Desired Value 4";
            popup_chkbox_first.IsChecked = Home.PositionDesiredRecordStatus[0];
            popup_chkbox_second.IsChecked = Home.PositionDesiredRecordStatus[1];
            popup_chkbox_third.IsChecked = Home.PositionDesiredRecordStatus[2];
            popup_chkbox_forth.IsChecked = Home.PositionDesiredRecordStatus[3];
            popup_record_setting_number = 4;
        }

        private void btn_popup_loadcell_desire_value_Click(object sender, RoutedEventArgs e)
        {
            popup_chkbox_first.Content = "loadCell Desired Value 1";
            popup_chkbox_second.Content = "loadCell Desired Value 2";
            popup_chkbox_third.Content = "loadCell Desired Value 3";
            popup_chkbox_forth.Content = "loadCell Desired Value 4";
            popup_chkbox_first.IsChecked = Home.LoadCellDesiredRecordStatus[0];
            popup_chkbox_second.IsChecked = Home.LoadCellDesiredRecordStatus[1];
            popup_chkbox_third.IsChecked = Home.LoadCellDesiredRecordStatus[2];
            popup_chkbox_forth.IsChecked = Home.LoadCellDesiredRecordStatus[3];
            popup_record_setting_number = 5;
        }

        private void btn_popup_emg_right_value_Click(object sender, RoutedEventArgs e)
        {
            popup_chkbox_first.Content = "Emg Right Value 1";
            popup_chkbox_second.Content = "Emg Right Value 2";
            popup_chkbox_third.Content = "Emg Right Value 3";
            popup_chkbox_forth.Content = "Emg Right Value 4";
            popup_chkbox_first.IsChecked = Home.EmgRightRecordStatus[0];
            popup_chkbox_second.IsChecked = Home.EmgRightRecordStatus[1];
            popup_chkbox_third.IsChecked = Home.EmgRightRecordStatus[2];
            popup_chkbox_forth.IsChecked = Home.EmgRightRecordStatus[3];
            popup_record_setting_number = 6;
        }

        private void btn_popup_emg_left_value_Click(object sender, RoutedEventArgs e)
        {
            popup_chkbox_first.Content = "Emg Left Value 1";
            popup_chkbox_second.Content = "Emg Left Value 2";
            popup_chkbox_third.Content = "Emg Left Value 3";
            popup_chkbox_forth.Content = "Emg Left Value 4";
            popup_chkbox_first.IsChecked = Home.EmgLeftRecordStatus[0];
            popup_chkbox_second.IsChecked = Home.EmgLeftRecordStatus[1];
            popup_chkbox_third.IsChecked = Home.EmgLeftRecordStatus[2];
            popup_chkbox_forth.IsChecked = Home.EmgLeftRecordStatus[3];
            popup_record_setting_number = 7;
        }

        private void btn_cancel_record_popup_Click(object sender, RoutedEventArgs e)
        {
            Record_popup.IsOpen = false;
        }

        private void btn_ok_record_popup_Click(object sender, RoutedEventArgs e)
        {
            Record_popup.IsOpen = false;
            Home.start_record_flag = true;
            Home.is_change_record_flag = true;
            btn_record.Content = "STOP";
            btn_record.Background = (System.Windows.Media.Brush)Application.Current.Resources["SuccessBrush"];
        }

        private void popup_chkbox_first_Click(object sender, RoutedEventArgs e)
        {
            switch(popup_record_setting_number)
            {
                case 0:
                    Home.PositionActualRecordStatus[0] = (bool)popup_chkbox_first.IsChecked;
                    break;
                case 1:
                    Home.CurrentActualRecordStatus[0] = (bool)popup_chkbox_first.IsChecked;
                    break;
                case 2:
                    Home.LoadCellActualRecordStatus[0] = (bool)popup_chkbox_first.IsChecked;
                    break;
                case 3:
                    Home.FootSensorActualRecordStatus[0] = (bool)popup_chkbox_first.IsChecked;
                    break;
                case 4:
                    Home.PositionDesiredRecordStatus[0] = (bool)popup_chkbox_first.IsChecked;
                    break;
                case 5:
                    Home.LoadCellDesiredRecordStatus[0] = (bool)popup_chkbox_first.IsChecked;
                    break;
                case 6:
                    Home.EmgRightRecordStatus[0] = (bool)popup_chkbox_first.IsChecked;
                    break;
                case 7:
                    Home.EmgLeftRecordStatus[0] = (bool)popup_chkbox_first.IsChecked;
                    break;
            }
        }

        private void popup_chkbox_second_Click(object sender, RoutedEventArgs e)
        {
            switch (popup_record_setting_number)
            {
                case 0:
                    Home.PositionActualRecordStatus[1] = (bool)popup_chkbox_second.IsChecked;
                    break;
                case 1:
                    Home.CurrentActualRecordStatus[1] = (bool)popup_chkbox_second.IsChecked;
                    break;
                case 2:
                    Home.LoadCellActualRecordStatus[1] = (bool)popup_chkbox_second.IsChecked;
                    break;
                case 3:
                    Home.FootSensorActualRecordStatus[1] = (bool)popup_chkbox_second.IsChecked;
                    break;
                case 4:
                    Home.PositionDesiredRecordStatus[1] = (bool)popup_chkbox_second.IsChecked;
                    break;
                case 5:
                    Home.LoadCellDesiredRecordStatus[1] = (bool)popup_chkbox_second.IsChecked;
                    break;
                case 6:
                    Home.EmgRightRecordStatus[1] = (bool)popup_chkbox_second.IsChecked;
                    break;
                case 7:
                    Home.EmgLeftRecordStatus[1] = (bool)popup_chkbox_second.IsChecked;
                    break;
            }
        }

        private void popup_chkbox_third_Click(object sender, RoutedEventArgs e)
        {
            switch (popup_record_setting_number)
            {
                case 0:
                    Home.PositionActualRecordStatus[2] = (bool)popup_chkbox_third.IsChecked;
                    break;
                case 1:
                    Home.CurrentActualRecordStatus[2] = (bool)popup_chkbox_third.IsChecked;
                    break;
                case 2:
                    Home.LoadCellActualRecordStatus[2] = (bool)popup_chkbox_third.IsChecked;
                    break;
                case 3:
                    Home.FootSensorActualRecordStatus[2] = (bool)popup_chkbox_third.IsChecked;
                    break;
                case 4:
                    Home.PositionDesiredRecordStatus[2] = (bool)popup_chkbox_third.IsChecked;
                    break;
                case 5:
                    Home.LoadCellDesiredRecordStatus[2] = (bool)popup_chkbox_third.IsChecked;
                    break;
                case 6:
                    Home.EmgRightRecordStatus[2] = (bool)popup_chkbox_third.IsChecked;
                    break;
                case 7:
                    Home.EmgLeftRecordStatus[2] = (bool)popup_chkbox_third.IsChecked;
                    break;
            }
        }

        private void popup_chkbox_forth_Click(object sender, RoutedEventArgs e)
        {
            switch (popup_record_setting_number)
            {
                case 0:
                    Home.PositionActualRecordStatus[3] = (bool)popup_chkbox_forth.IsChecked;
                    break;
                case 1:
                    Home.CurrentActualRecordStatus[3] = (bool)popup_chkbox_forth.IsChecked;
                    break;
                case 2:
                    Home.LoadCellActualRecordStatus[3] = (bool)popup_chkbox_forth.IsChecked;
                    break;
                case 3:
                    Home.FootSensorActualRecordStatus[3] = (bool)popup_chkbox_forth.IsChecked;
                    break;
                case 4:
                    Home.PositionDesiredRecordStatus[3] = (bool)popup_chkbox_forth.IsChecked;
                    break;
                case 5:
                    Home.LoadCellDesiredRecordStatus[3] = (bool)popup_chkbox_forth.IsChecked;
                    break;
                case 6:
                    Home.EmgRightRecordStatus[3] = (bool)popup_chkbox_forth.IsChecked;
                    break;
                case 7:
                    Home.EmgLeftRecordStatus[3] = (bool)popup_chkbox_forth.IsChecked;
                    break;
            }
        }

        private void btn_start_main_grid_Click(object sender, RoutedEventArgs e)
        {
            main_grid.Visibility = Visibility.Visible;
            demo_grid.Visibility = Visibility.Hidden;

            Home.turn_on_flag = true;
            LoadCell.update_flag = true;
            FootSensor.update_flag = true;
            btn_record.IsEnabled = true;
            btn_run.IsEnabled = true;
            btn_clear.IsEnabled = true;
            btn_stop.IsEnabled = true;

        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            main_grid.Visibility = Visibility.Hidden;
            demo_grid.Visibility = Visibility.Visible;
        }
    }
}
