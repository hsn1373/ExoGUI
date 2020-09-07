using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace ExoGUI.MainSide
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cmb_loadcells.Items.Add(port);
                cmb_foot_sensor_left.Items.Add(port);
                cmb_foot_sensor_right.Items.Add(port);
                cmb_tredmil.Items.Add(port);
            }
            cmb_loadcells.Text = ports[1];
            BeckhoffContext.LoadCell_Com_Port_Name = cmb_loadcells.Text;
            cmb_foot_sensor_left.Text = ports[3];
            BeckhoffContext.FootSensorLeft_Com_Port_Name = cmb_foot_sensor_left.Text;
            cmb_foot_sensor_right.Text = ports[2];
            BeckhoffContext.FootSensorRight_Com_Port_Name = cmb_foot_sensor_right.Text;
            cmb_tredmil.Text = ports[0];

            BeckhoffContext.Assist_Algorithm = "zero_impedance";


            string[] assist_algorithms = { "Assist_As_Need", "Inverse_Dynamic", "EMG", "Impedance","Torque_control" };
            foreach (string assist_algorithm in assist_algorithms)
            {
                cmb_assist_algorithm.Items.Add(assist_algorithm);
            }
            cmb_assist_algorithm.Text = assist_algorithms[0];
        }

        private void btn_config_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.LoadCell_Com_Port_Name = cmb_loadcells.Text;
            BeckhoffContext.FootSensorLeft_Com_Port_Name = cmb_foot_sensor_left.Text;
            BeckhoffContext.FootSensorRight_Com_Port_Name = cmb_foot_sensor_right.Text;
            BeckhoffContext.Tredmil_Com_Port_Name = cmb_tredmil.Text;
            if(rdb_zero_impedance.IsChecked==true)
            {
                BeckhoffContext.Assist_Algorithm = "zero_impedance";
            }
            else
            {
                BeckhoffContext.Assist_Algorithm = cmb_assist_algorithm.Text;
            }
        }

        private void rdb_zero_impedance_Click(object sender, RoutedEventArgs e)
        {
            lbl_assist_algorithm.IsEnabled = false;
            cmb_assist_algorithm.IsEnabled = false;
            lbl_zero_impedance.IsEnabled = true;
        }

        private void rdb_assist_algorithm_Click(object sender, RoutedEventArgs e)
        {
            lbl_assist_algorithm.IsEnabled = true;
            cmb_assist_algorithm.IsEnabled = true;
            lbl_zero_impedance.IsEnabled = false;
        }
    }
}
