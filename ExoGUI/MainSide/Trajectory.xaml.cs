using ExoGUI.NetWork;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ExoGUI.MainSide
{
    /// <summary>
    /// Interaction logic for Trajectory.xaml
    /// </summary>
    public partial class Trajectory : UserControl
    {
        UInt32 start_traj_len = 0;
        UInt32 right_traj_len = 0;
        string current_gain = "1";
        public Thread th;

        public Trajectory()
        {
            InitializeComponent();
            string[] speeds = { "1", "2", "3", "4" };
            foreach (string speed in speeds)
            {
                cmb_speed.Items.Add(speed);
            }
            cmb_speed.Text = speeds[0];
        }

        private void btn_select_file_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                start_traj_len = 0;
                right_traj_len = 0;
                OpenFileDialog PositionTrajectoryFileDialog = new OpenFileDialog();
                if (PositionTrajectoryFileDialog.ShowDialog() == true)
                {
                    string filePath = PositionTrajectoryFileDialog.FileName;

                    using (var reader = new StreamReader(filePath))
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        start_traj_len = Convert.ToUInt32(values[0]);
                        right_traj_len = Convert.ToUInt32(values[1]);
                        line = reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            values = line.Split(',');
                            BeckhoffContext.Controller.fillBuffers(values);
                        }
                    }

                    BeckhoffContext.Controller.StartTrajLen = start_traj_len;
                    BeckhoffContext.Controller.RightTrajLen = right_traj_len;
                    btn_start_traj.IsEnabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_start_traj_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Controller.EnableButtons = 0;
            btn_left_traj.IsEnabled = false;
            btn_start_traj.IsEnabled = false;
            btn_stop_traj.IsEnabled = true;
            btn_increase_gain.IsEnabled = true;
            btn_decrease_gain.IsEnabled = true;
            current_gain = txt_gain.Text;
            BeckhoffContext.Controller.TrajectoryGain = float.Parse(txt_gain.Text);
            BeckhoffContext.Controller.sendStartTrajFirstBuffer(Convert.ToInt32(cmb_speed.Text));
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["start_trajectory"];
            th = new Thread(Run_Config_Buttons_Enable);
            th.Start();
        }

        private void btn_left_traj_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Controller.EnableButtons = 0;
            btn_left_traj.IsEnabled = false;
            btn_increase_gain.IsEnabled = true;
            btn_decrease_gain.IsEnabled = true;
            current_gain = txt_gain.Text;
            BeckhoffContext.Controller.TrajectoryGain = float.Parse(txt_gain.Text);
            BeckhoffContext.Controller.sendLeftTrajFirstBuffer(Convert.ToInt32(cmb_speed.Text));
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["left_trajectory"];
            th = new Thread(Run_Config_Buttons_Enable);
            th.Start();
        }

        private void btn_right_traj_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Controller.EnableButtons = 0;
            btn_right_traj.IsEnabled = false;
            btn_increase_gain.IsEnabled = true;
            btn_decrease_gain.IsEnabled = true;
            current_gain = txt_gain.Text;
            BeckhoffContext.Controller.TrajectoryGain = float.Parse(txt_gain.Text);
            BeckhoffContext.Controller.sendRightTrajFirstBuffer(Convert.ToInt32(cmb_speed.Text));
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["right_trajectory"];
            th = new Thread(Run_Config_Buttons_Enable);
            th.Start();
        }

        private void btn_stop_traj_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Controller.EnableButtons = 0;
            btn_start_traj.IsEnabled = true;
            btn_contnious_trajectory.IsEnabled = false;
            btn_stop_traj.IsEnabled = false;
            btn_right_traj.IsEnabled = false;
            btn_left_traj.IsEnabled = false;
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["stop_trajectory"];
        }

        private void txt_gain_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txt_gain.Text != "" || txt_gain.Text != null)
                {
                    double temp = Convert.ToDouble(txt_gain.Text);
                    if (0.0 < temp && temp > 1.0)
                    {
                        lbl_validation.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        lbl_validation.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void cmb_speed_DropDownClosed(object sender, EventArgs e)
        {
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["do_nothing"];
            BeckhoffContext.Controller.StartTrajLen = start_traj_len / Convert.ToUInt32(cmb_speed.Text);
            BeckhoffContext.Controller.RightTrajLen = right_traj_len / Convert.ToUInt32(cmb_speed.Text);
            BeckhoffContext.Controller.TrajectorySpeed = Convert.ToInt32(cmb_speed.Text);
        }

        private void btn_increase_gain_Click(object sender, RoutedEventArgs e)
        {
            if((Convert.ToDouble(txt_gain.Text) + 0.1).ToString()==current_gain)
            {
                btn_decrease_gain.IsEnabled = true;
                txt_gain.Text = (Convert.ToDouble(txt_gain.Text) + 0.1).ToString();
            }
            else if((Convert.ToDouble(txt_gain.Text) + 0.1).ToString() == "1.1")
            {
                btn_decrease_gain.IsEnabled = true;
                btn_increase_gain.IsEnabled = false;
            }
            else
            {
                btn_decrease_gain.IsEnabled = true;
                btn_increase_gain.IsEnabled = false;
                txt_gain.Text = (Convert.ToDouble(txt_gain.Text) + 0.1).ToString();
            }
        }

        private void btn_decrease_gain_Click(object sender, RoutedEventArgs e)
        {
            if ((Convert.ToDouble(txt_gain.Text) - 0.1).ToString() == current_gain)
            {
                btn_increase_gain.IsEnabled = true;
                txt_gain.Text = (Convert.ToDouble(txt_gain.Text) - 0.1).ToString();
            }
            else if ((Convert.ToDouble(txt_gain.Text) - 0.1).ToString() == "-0.1")
            {
                btn_decrease_gain.IsEnabled = false;
                btn_increase_gain.IsEnabled = true;
            }
            else
            {
                btn_increase_gain.IsEnabled = true;
                btn_decrease_gain.IsEnabled = false;
                txt_gain.Text = (Convert.ToDouble(txt_gain.Text) - 0.1).ToString();
            }
        }
        private void Run_Config_Buttons_Enable()
        {
            while (true)
            {
                switch (BeckhoffContext.Controller.EnableButtons)
                {
                    case 1:
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            btn_left_traj.IsEnabled = true;
                            btn_contnious_trajectory.IsEnabled = true;
                        }));
                        th.Abort();
                        return;
                    case 2:
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            btn_right_traj.IsEnabled = true;
                        }));
                        th.Abort();
                        return;
                    case 3:
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            btn_left_traj.IsEnabled = true;
                        }));
                        th.Abort();
                        return;
                    case 4:
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["do_nothing"];
                            BeckhoffContext.Controller.sendLeftTrajFirstBuffer(Convert.ToInt32(cmb_speed.Text));
                            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["continous_trajectory"];
                        }));
                        break;
                }
            }
        }

        private void btn_contnious_trajectory_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Controller.EnableButtons = 0;
            btn_contnious_trajectory.IsEnabled = false;
            btn_left_traj.IsEnabled = false;
            btn_start_traj.IsEnabled = false;
            btn_stop_traj.IsEnabled = true;
            btn_increase_gain.IsEnabled = true;
            btn_decrease_gain.IsEnabled = true;
            current_gain = txt_gain.Text;
            BeckhoffContext.Controller.TrajectoryGain = float.Parse(txt_gain.Text);
            BeckhoffContext.Controller.sendLeftTrajFirstBuffer(Convert.ToInt32(cmb_speed.Text));
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["continous_trajectory"];
            th = new Thread(Run_Config_Buttons_Enable);
            th.Start();
        }
    }
}
