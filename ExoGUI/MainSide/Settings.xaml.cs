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
            BeckhoffContext.Assist_Algorithm = "zero_impedance";
        }

        private void rdb_zero_impedance_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Assist_Algorithm = "zero_impedance";
        }

        private void rdb_assist_as_needed_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Assist_Algorithm = "Assist_As_Need";
        }

        private void rdb_inverse_dynamic_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Assist_Algorithm = "Inverse_Dynamic";
        }

        private void rdb_emg_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Assist_Algorithm = "EMG";
        }

        private void rdb_impedance_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Assist_Algorithm = "Impedance";
        }

        private void rdb_torque_control_Click(object sender, RoutedEventArgs e)
        {
            BeckhoffContext.Assist_Algorithm = "Torque_control";
        }
    }
}
