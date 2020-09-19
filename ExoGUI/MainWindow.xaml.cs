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
            grd_home.Visibility = System.Windows.Visibility.Visible;
            grd_loadcell.Visibility = System.Windows.Visibility.Hidden;
            grd_footsensor.Visibility = System.Windows.Visibility.Hidden;
            grd_trajectory.Visibility = System.Windows.Visibility.Hidden;
            grd_settings.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btn_load_cell_Click(object sender, RoutedEventArgs e)
        {
            grd_home.Visibility = System.Windows.Visibility.Hidden;
            grd_loadcell.Visibility = System.Windows.Visibility.Visible;
            grd_footsensor.Visibility = System.Windows.Visibility.Hidden;
            grd_trajectory.Visibility = System.Windows.Visibility.Hidden;
            grd_settings.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btn_foot_sensor_Click(object sender, RoutedEventArgs e)
        {
            grd_home.Visibility = System.Windows.Visibility.Hidden;
            grd_loadcell.Visibility = System.Windows.Visibility.Hidden;
            grd_footsensor.Visibility = System.Windows.Visibility.Visible;
            grd_trajectory.Visibility = System.Windows.Visibility.Hidden;
            grd_settings.Visibility = System.Windows.Visibility.Hidden;
        }
        private void btn_trajectory_Click(object sender, RoutedEventArgs e)
        {
            grd_home.Visibility = System.Windows.Visibility.Hidden;
            grd_loadcell.Visibility = System.Windows.Visibility.Hidden;
            grd_footsensor.Visibility = System.Windows.Visibility.Hidden;
            grd_trajectory.Visibility = System.Windows.Visibility.Visible;
            grd_settings.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btn_settings_Click(object sender, RoutedEventArgs e)
        {
            grd_home.Visibility = System.Windows.Visibility.Hidden;
            grd_loadcell.Visibility = System.Windows.Visibility.Hidden;
            grd_footsensor.Visibility = System.Windows.Visibility.Hidden;
            grd_trajectory.Visibility = System.Windows.Visibility.Hidden;
            grd_settings.Visibility = System.Windows.Visibility.Visible;
        }

        private void btn_run_Click(object sender, RoutedEventArgs e)
        {

            if(btn_run.Content.ToString() == "RUN DRIVERS")
            {
                btn_run.Content = "RUN";
                btn_run.Background = (System.Windows.Media.Brush)Application.Current.Resources["WarningBrush"];
                BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["run"];
            }
            else if(btn_run.Content.ToString() == "RUN")
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
            btn_run.Content = "RUN DRIVERS";
            btn_run.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["stop"];
        }

        private void btn_turn_on_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Dispatcher.Invoke(new Action(() =>
            //{
            //    mut.WaitOne();
            if (btn_turn_on.Content.ToString() == "TURN ON")
            {
               // LoadCell.open_port();
                Home.turn_on_flag = true;
                LoadCell.update_flag = true;
                //FootSensor.open_port();
                FootSensor.update_flag = true;
                btn_turn_on.Content = "TURN OFF";
                btn_turn_on.Background = (System.Windows.Media.Brush)Application.Current.Resources["SuccessBrush"];
                btn_record.IsEnabled = true;
                btn_run.IsEnabled = true;
                btn_clear.IsEnabled = true;
                btn_stop.IsEnabled = true;
            }
            else
            {
                //LoadCell.close_port();
                Home.turn_on_flag = false;
                LoadCell.update_flag = false;
                //FootSensor.close_port();
                FootSensor.update_flag = false;
                btn_turn_on.Content = "TURN ON";
                btn_turn_on.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
                btn_record.IsEnabled = false;
                btn_run.IsEnabled = false;
                btn_clear.IsEnabled = false;
                btn_stop.IsEnabled = false;
            }
            //    mut.ReleaseMutex();
            //}));
        }

        private void btn_record_Click(object sender, RoutedEventArgs e)
        {
            //if (btn_record.Content.ToString() == "RECORD")
            //{
            //    Home.start_record_flag = true;
            //    Home.is_change_record_flag = true;
            //    btn_record.Content = "STOP";
            //    btn_record.Background = (System.Windows.Media.Brush)Application.Current.Resources["SuccessBrush"];
            //}
            //else
            //{
            //    Home.start_record_flag = false;
            //    //for (int i = 0; i < Home.ActualPos1RecordedData.Count; i++)
            //    //    Console.WriteLine(Home.ActualPos1RecordedData[i]);
            //    Home.is_change_record_flag = true;
            //    btn_record.Content = "RECORD";
            //    btn_record.Background = (System.Windows.Media.Brush)Application.Current.Resources["WindowBackgroundColor"];
            //    Application.Current.Dispatcher.Invoke(new Action(() =>
            //    {
            //        WriteToExcel("D:\\csharp-Excel1.xls", Home.ActualPos1RecordedData, Home.ActualPos2RecordedData, Home.ActualPos3RecordedData, Home.ActualPos4RecordedData,
            //        Home.CurrentVal1RecordedData, Home.CurrentVal2RecordedData, Home.CurrentVal3RecordedData, Home.CurrentVal4RecordedData,
            //        Home.LoadCell1RecordedData, Home.LoadCell2RecordedData, Home.LoadCell3RecordedData, Home.LoadCell4RecordedData,
            //        Home.FootSensor1RecordedData,Home.FootSensor2RecordedData,Home.FootSensor3RecordedData,Home.FootSensor4RecordedData);
            //    }));
            //}

            Record_popup.IsOpen = true;

        }
        public void WriteToExcel(string Addres, List<Int32> _Apos1, List<Int32> _Apos2, List<Int32> _Apos3, List<Int32> _Apos4,
           List<Int16> _CurrentVal1, List<Int16> _CurrentVal2, List<Int16> _CurrentVal3, List<Int16> _CurrentVal4,
           List<Int32> _LoadCell1, List<Int32> _LoadCell2, List<Int32> _LoadCell3, List<Int32> _LoadCell4,
           List<Int32> _FootSensor1, List<Int32> _FootSensor2, List<Int32> _FootSensor3, List<Int32> _FootSensor4)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }


                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Cells[1, 1] = "Actual Pos1";
                xlWorkSheet.Cells[1, 2] = "Actual Pos2";
                xlWorkSheet.Cells[1, 3] = "Actual Pos3";
                xlWorkSheet.Cells[1, 4] = "Actual Pos4";

                xlWorkSheet.Cells[1, 6] = "Current Torque Value1";
                xlWorkSheet.Cells[1, 7] = "Current Torque Value2";
                xlWorkSheet.Cells[1, 8] = "Current Torque Value3";
                xlWorkSheet.Cells[1, 9] = "Current Torque Value4";

                xlWorkSheet.Cells[1, 11] = "Load Cell1";
                xlWorkSheet.Cells[1, 12] = "Load Cell2";
                xlWorkSheet.Cells[1, 13] = "Load Cell3";
                xlWorkSheet.Cells[1, 14] = "Load Cell4";

                xlWorkSheet.Cells[1, 15] = "Foot Sensor1";
                xlWorkSheet.Cells[1, 16] = "Foot Sensor2";
                xlWorkSheet.Cells[1, 17] = "Foot Sensor3";
                xlWorkSheet.Cells[1, 18] = "Foot Sensor4";
                int min = Math.Min(_Apos1.Count, _CurrentVal1.Count);
                int min2 = Math.Min(min, _LoadCell1.Count);
                int min3 = Math.Min(min2, _FootSensor1.Count);
                for (int i = 0; i < min3; i++)
                {
                    xlWorkSheet.Cells[i + 2, 1] = _Apos1[i].ToString();
                    xlWorkSheet.Cells[i + 2, 2] = _Apos2[i].ToString();
                    xlWorkSheet.Cells[i + 2, 3] = _Apos3[i].ToString();
                    xlWorkSheet.Cells[i + 2, 4] = _Apos4[i].ToString();

                    xlWorkSheet.Cells[i + 2, 6] = _CurrentVal1[i].ToString();
                    xlWorkSheet.Cells[i + 2, 7] = _CurrentVal2[i].ToString();
                    xlWorkSheet.Cells[i + 2, 8] = _CurrentVal3[i].ToString();
                    xlWorkSheet.Cells[i + 2, 9] = _CurrentVal4[i].ToString();

                    xlWorkSheet.Cells[i + 2, 11] = _LoadCell1[i].ToString();
                    xlWorkSheet.Cells[i + 2, 12] = _LoadCell2[i].ToString();
                    xlWorkSheet.Cells[i + 2, 13] = _LoadCell3[i].ToString();
                    xlWorkSheet.Cells[i + 2, 14] = _LoadCell4[i].ToString();

                    xlWorkSheet.Cells[i + 2, 15] = _FootSensor1[i].ToString();
                    xlWorkSheet.Cells[i + 2, 16] = _FootSensor2[i].ToString();
                    xlWorkSheet.Cells[i + 2, 17] = _FootSensor3[i].ToString();
                    xlWorkSheet.Cells[i + 2, 18] = _FootSensor4[i].ToString();
                }
                //xlWorkSheet.Cells[1, 1] = "ID";
                //xlWorkSheet.Cells[1, 2] = "Name";
                //xlWorkSheet.Cells[2, 1] = "1";
                //xlWorkSheet.Cells[2, 2] = "One";
                //xlWorkSheet.Cells[3, 1] = "2";
                //xlWorkSheet.Cells[3, 2] = "Two";



                xlWorkBook.SaveAs(Addres, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                MessageBox.Show("Excel file created , you can find the file" + Addres);
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
    }
}
