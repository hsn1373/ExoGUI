using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoGUI.NetWork
{
    public class BeckhoffContext
    {
       public static Dictionary<string, UInt32> gui_manager_keys = new Dictionary<string, UInt32>()
        {
            {"run",1},
            {"clear_error",2},
            {"home",3},
            {"stop",4},
            {"position_mode_point",5},
            {"start_trajectory",6},
            {"Assist_As_Need",7},
            {"Impedance",8},
            {"Inverse_Dynamic",9},
            {"EMG",10},
            {"zero_impedance",11},
            {"Torque_control",12 },
            {"right_trajectory",13},
            {"left_trajectory",14},
            {"stop_trajectory",15},
            {"continous_trajectory",16},
            {"do_nothing",17}
        };

        public static string LoadCell_Com_Port_Name { get; set; }

        public static string FootSensorLeft_Com_Port_Name { get; set; }

        public static string FootSensorRight_Com_Port_Name { get; set; }

        public static string Tredmil_Com_Port_Name { get; set; }

        public static string Assist_Algorithm { get; set; }

        public static MyController Controller { get; set; }
        public static string BeckhoffAddress { get; set; }
        public static int BeckhoffPort { get; set; }
        public BeckhoffContext(string _beckhoffAddress, int _port)
        {
            BeckhoffAddress = _beckhoffAddress;
            BeckhoffPort = _port;
        }

        public void StartController()
        {
            Controller = new MyController(BeckhoffAddress, BeckhoffPort);
        }
    }
}
