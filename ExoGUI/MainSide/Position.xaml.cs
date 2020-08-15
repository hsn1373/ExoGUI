using ExoGUI.NetWork;
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

namespace ExoGUI.MainSide
{
    /// <summary>
    /// Interaction logic for Position.xaml
    /// </summary>
    public partial class Position : UserControl
    {
        public Position()
        {
            InitializeComponent();
        }

        private void btn_position_point_submit_Click(object sender, RoutedEventArgs e)
        {
            //BeckhoffContext.Controller.Position_mode_right_hip = Convert.ToSingle(txt_right_hip.Text);
            //BeckhoffContext.Controller.Position_mode_right_knee = Convert.ToSingle(txt_right_knee.Text);
            //BeckhoffContext.Controller.Position_mode_left_hip = Convert.ToSingle(txt_left_hip.Text);
            //BeckhoffContext.Controller.Position_mode_left_knee = Convert.ToSingle(txt_left_knee.Text);
            //BeckhoffContext.Controller.Gui_manager = BeckhoffContext.gui_manager_keys["position_mode_point"];
        }
    }
}
