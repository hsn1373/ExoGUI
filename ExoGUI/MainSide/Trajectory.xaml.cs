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

namespace ExoGUI.MainSide
{
    /// <summary>
    /// Interaction logic for Trajectory.xaml
    /// </summary>
    public partial class Trajectory : UserControl
    {
        public Trajectory()
        {
            InitializeComponent();
            string[] speeds = { "1", "2", "5", "10" };
            foreach (string speed in speeds)
            {
                cmb_speed.Items.Add(speed);
            }
            cmb_speed.Text = speeds[0];
        }

        private void btn_select_file_Click(object sender, RoutedEventArgs e)
        {
            int trajLen = 0;
            OpenFileDialog PositionTrajectoryFileDialog = new OpenFileDialog();
            if (PositionTrajectoryFileDialog.ShowDialog() == true)
            {
                string filePath = PositionTrajectoryFileDialog.FileName;

                using (var reader = new StreamReader(filePath))
                {
                    var line = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        var values = line.Split(',');
                        BeckhoffContext.Controller.fillBuffers(values);
                        trajLen++;
                        /*
                        leftHipDatalist.Add(Convert.ToSingle(values[0]));
                        leftKneeDatalist.Add(Convert.ToSingle(values[1]));
                        RightHipDatalist.Add(Convert.ToSingle(values[2]));
                        RightKneeDatalist.Add(Convert.ToSingle(values[3]));
                        */
                    }
                }

                BeckhoffContext.Controller.TrajLen = (UInt32)trajLen;

                /*
                float[,] _buffer = new float[4, 500];
                BeckhoffContext.Controller.BufferCounter = 0;
                // fill first buffer
                for (int i = 0; i < 500; i++)
                {
                    _buffer[0, i] = leftHipDatalist[i];
                    _buffer[1, i] = leftKneeDatalist[i];
                    _buffer[2, i] = RightHipDatalist[i];
                    _buffer[3, i] = RightKneeDatalist[i];
                }
                BeckhoffContext.Controller.BufferPos1 = _buffer;
                BeckhoffContext.Controller.BufferCounter = BeckhoffContext.Controller.BufferCounter + 500;
                */
                BeckhoffContext.Controller.sendFirstBuffer();
            }
        }

        private void btn_trajectory_run_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["position_mode_trajectory"];
        }

        private void btn_start_traj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_left_traj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_right_traj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_stop_traj_Click(object sender, RoutedEventArgs e)
        {

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
                        lbl_validation.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        lbl_validation.Visibility = System.Windows.Visibility.Hidden;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
