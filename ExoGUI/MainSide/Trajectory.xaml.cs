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
    }
}
