using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoGUI.MainSide;

namespace ExoGUI.NetWork
{
    public class MyController : IDisposable
    {
        List<float> leftHipDatalist = new List<float>();
        List<float> leftKneeDatalist = new List<float>();
        List<float> RightHipDatalist = new List<float>();
        List<float> RightKneeDatalist = new List<float>();
        private float[,] _bufferPos1 = new float[4, 500];
        private float[,] _bufferPos2 = new float[4, 500];
        private int _bufferCounter = 0;
        private bool _reachEndOfTraj;

        private int _trajectory_speed = 1;
        private float _trajectory_gain = 1.00f;
        private bool _is_left_or_right_button_clicked = false;
        
        private PLCConnection _connection;
        private UInt32 _gui_manager;
        private float _position_mode_right_hip;
        private float _position_mode_right_knee;
        private float _position_mode_left_hip;
        private float _position_mode_left_knee;
        private UInt32 _buffer_status, _whichEndOfTraj;
        private UInt32 _start_traj_len,_right_traj_len;
        private bool _read_actual_pos;
        private Int32 _target;
        private Int32 _target2;
        private Int32 _target3;
        private Int32 _target4;
        public int EnableButtons = 0;

        public int BufferCounter
        {
            get
            {
                return _bufferCounter;
            }
            set
            {
                _bufferCounter = value;
            }
        }
        public int TrajectorySpeed
        {
            get
            {
                return _trajectory_speed;
            }
            set
            {
                _trajectory_speed = value;
            }
        }

        public float TrajectoryGain
        {
            get
            {
                return _trajectory_gain;
            }
            set
            {
                _trajectory_gain = value;
            }
        }

        public float[,] BufferPos1
        {
            get
            {
                _bufferPos1 = (float[,])_connection[X.BufferPos1];
                return _bufferPos1;
            }
            set
            {
                _bufferPos1 = value;
                _connection[X.BufferPos1] = _bufferPos1;
            }
        }
        public float[,] BufferPos2
        {
            get
            {
                _bufferPos2 = (float[,])_connection[X.BufferPos2];
                return _bufferPos2;
            }
            set
            {
                _bufferPos2 = value;
                _connection[X.BufferPos2] = _bufferPos2;
            }
        }

        public bool ReadActualPos
        {
            get
            {
                _read_actual_pos = (bool)_connection[X.ReadActualPos];
                return _read_actual_pos;
            }
            set
            {
                _read_actual_pos = value;
                _connection[X.ReadActualPos] = _read_actual_pos;
                // OnPropertyChanged(nameof(ReadActualPos));
            }
        }

        public bool ReachEndOfTraj
        {
            get
            {
                _reachEndOfTraj = (bool)_connection[X.ReachEndOfTraj];
                return _reachEndOfTraj;
            }
            set
            {
                _reachEndOfTraj = value;
                _connection[X.ReachEndOfTraj] = _reachEndOfTraj;
            }
        }

        public UInt32 WhichEndOfTraj
        {
            get
            {
                _whichEndOfTraj = (UInt32)_connection[X.WhichEndOfTraj];
                return _whichEndOfTraj;
            }
            set
            {
                _whichEndOfTraj = value;
                _connection[X.WhichEndOfTraj] = _whichEndOfTraj;
            }
        }

        public int TargetPosition
        {
            get
            {
                _target = (int)_connection[X.TargetPosition];
                return _target;
            }
            set
            {
                _target = value;
                _connection[X.TargetPosition] = _target;
                //OnPropertyChanged(nameof(TargetPosition));
            }
        }
        public int TargetPosition2
        {
            get
            {
                _target2 = (int)_connection[X.TargetPosition2];
                return _target2;
            }
            set
            {
                _target2 = value;
                _connection[X.TargetPosition2] = _target2;
            }
        }
        public int TargetPosition3
        {
            get
            {
                _target3 = (int)_connection[X.TargetPosition3];
                return _target3;
            }
            set
            {
                _target3 = value;
                _connection[X.TargetPosition3] = _target3;
            }
        }
        public int TargetPosition4
        {
            get
            {
                _target4 = (int)_connection[X.TargetPosition4];
                return _target4;
            }
            set
            {
                _target4 = value;
                _connection[X.TargetPosition4] = _target4;
            }
        }
        public UInt32 Gui_manager
        {
            get
            {
                _gui_manager = (UInt32)_connection[X.Gui_manager];
                return _gui_manager;
            }
            set
            {
                _gui_manager = value;
                _connection[X.Gui_manager] = _gui_manager;
            }
        }
        public float Position_mode_right_hip
        {
            get
            {
                _position_mode_right_hip = (float)_connection[X.Position_mode_right_hip];
                return _position_mode_right_hip;
            }
            set
            {
                _position_mode_right_hip = value;
                _connection[X.Position_mode_right_hip] = _position_mode_right_hip;
            }
        }
        public float Position_mode_right_knee
        {
            get
            {
                _position_mode_right_knee = (float)_connection[X.Position_mode_right_knee];
                return _position_mode_right_knee;
            }
            set
            {
                _position_mode_right_knee = value;
                _connection[X.Position_mode_right_knee] = _position_mode_right_knee;
            }
        }
        public float Position_mode_left_hip
        {
            get
            {
                _position_mode_left_hip = (float)_connection[X.Position_mode_left_hip];
                return _position_mode_left_hip;
            }
            set
            {
                _position_mode_left_hip = value;
                _connection[X.Position_mode_left_hip] = _position_mode_left_hip;
            }
        }
        public float Position_mode_left_knee
        {
            get
            {
                _position_mode_left_knee = (float)_connection[X.Position_mode_left_knee];
                return _position_mode_left_knee;
            }
            set
            {
                _position_mode_left_knee = value;
                _connection[X.Position_mode_left_knee] = _position_mode_left_knee;
            }
        }
        public UInt32 StartTrajLen
        {
            get
            {
                _start_traj_len = (UInt32)_connection[X.StartTrajLen];
                return _start_traj_len;
            }
            set
            {
                _start_traj_len = value;
                _connection[X.StartTrajLen] = _start_traj_len;
            }
        }
        public UInt32 RightTrajLen
        {
            get
            {
                _right_traj_len = (UInt32)_connection[X.RightTrajLen];
                return _right_traj_len;
            }
            set
            {
                _right_traj_len = value;
                _connection[X.RightTrajLen] = _right_traj_len;
            }
        }

        public UInt32 BufferStatus
        {
            get
            {
                _buffer_status = (UInt32)_connection[X.BufferStatus];
                return _buffer_status;
            }
            set
            {
                _buffer_status = value;
                _connection[X.BufferStatus] = _buffer_status;
            }
        }
        public MyController(string beckhoffAddress, int port)
        {
            _connection = PLCConnection.getInstance(beckhoffAddress, port); //new PLCConnection(beckhoffAddress, port, bufferSize);
            CommonInitialize();
        }

        public void fillBuffers(string[] values)
        {
            leftHipDatalist.Add(Convert.ToSingle(values[0]));
            leftKneeDatalist.Add(Convert.ToSingle(values[1]));
            RightHipDatalist.Add(Convert.ToSingle(values[2]));
            RightKneeDatalist.Add(Convert.ToSingle(values[3]));
        }

        public void sendStartTrajFirstBuffer(int speed)
        {
            BufferCounter = 0;
            float[,] _buffer = new float[4, 500];
            float[,] _buffer2 = new float[4, 500];
            for (int i = 0; i < 500; i++)
            {
                _buffer[0, i] = TrajectoryGain * leftHipDatalist[i * speed];
                _buffer[1, i] = TrajectoryGain * leftKneeDatalist[i * speed];
                _buffer[2, i] = TrajectoryGain * RightHipDatalist[i * speed];
                _buffer[3, i] = TrajectoryGain * RightKneeDatalist[i * speed];
                _buffer2[0, i] = TrajectoryGain * leftHipDatalist[(i+500) * speed];
                _buffer2[1, i] = TrajectoryGain * leftKneeDatalist[(i + 500) * speed];
                _buffer2[2, i] = TrajectoryGain * RightHipDatalist[(i + 500) * speed];
                _buffer2[3, i] = TrajectoryGain * RightKneeDatalist[(i + 500) * speed];
            }
            BufferPos1 = _buffer;
            BufferPos2 = _buffer2;
            BufferCounter = (BufferCounter + 1000)*speed;
        }

        public void sendRightTrajFirstBuffer(int speed)
        {
            float[,] _buffer = new float[4, 500];
            float[,] _buffer2 = new float[4, 500];
            BufferCounter = Convert.ToInt32(_start_traj_len + _right_traj_len);
            int j = Convert.ToInt32(_start_traj_len + _right_traj_len);
            for (int i = 0; i < 500; i++)
            {
                _buffer[0, i] = TrajectoryGain * leftHipDatalist[j * speed];
                _buffer[1, i] = TrajectoryGain * leftKneeDatalist[j * speed];
                _buffer[2, i] = TrajectoryGain * RightHipDatalist[j * speed];
                _buffer[3, i] = TrajectoryGain * RightKneeDatalist[j * speed];
                _buffer2[0, i] = TrajectoryGain * leftHipDatalist[(j + 500) * speed];
                _buffer2[1, i] = TrajectoryGain * leftKneeDatalist[(j + 500) * speed];
                _buffer2[2, i] = TrajectoryGain * RightHipDatalist[(j + 500) * speed];
                _buffer2[3, i] = TrajectoryGain * RightKneeDatalist[(j + 500) * speed];
                j++;
            }
            BufferPos1 = _buffer;
            BufferPos2 = _buffer2;
            BufferCounter = (BufferCounter + 1000) * speed;
        }

        public void sendLeftTrajFirstBuffer(int speed)
        {
            float[,] _buffer = new float[4, 500];
            float[,] _buffer2 = new float[4, 500];
            BufferCounter = Convert.ToInt32(_start_traj_len);
            int j = Convert.ToInt32(_start_traj_len);
            for (int i = 0; i < 500; i++)
            {
                _buffer[0, i] = TrajectoryGain * leftHipDatalist[j * speed];
                _buffer[1, i] = TrajectoryGain * leftKneeDatalist[j * speed];
                _buffer[2, i] = TrajectoryGain * RightHipDatalist[j * speed];
                _buffer[3, i] = TrajectoryGain * RightKneeDatalist[j * speed];
                _buffer2[0, i] = TrajectoryGain * leftHipDatalist[(j + 500) * speed];
                _buffer2[1, i] = TrajectoryGain * leftKneeDatalist[(j + 500) * speed];
                _buffer2[2, i] = TrajectoryGain * RightHipDatalist[(j + 500) * speed];
                _buffer2[3, i] = TrajectoryGain * RightKneeDatalist[(j + 500) * speed];
                j++;
            }
            BufferPos1 = _buffer;
            BufferPos2 = _buffer2;
            BufferCounter = (BufferCounter + 1000) * speed;
        }

        private void CommonInitialize()
        {
            _connection.PropertyChanged += _connection_PropertyChanged;
            _connection.Strat();
        }

        private void _connection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == X.BufferStatus.ToString())
            {
                if (!ReachEndOfTraj)
                {
                    float[,] _buffer = new float[4, 500];
                    if (BufferStatus == 1) // finish process buffer2 on beckhoff
                    {
                        for (int i = 0; i < 500; i++)
                        {
                            _buffer[0, i] = TrajectoryGain * leftHipDatalist[BufferCounter * TrajectorySpeed];
                            _buffer[1, i] = TrajectoryGain * leftKneeDatalist[BufferCounter * TrajectorySpeed];
                            _buffer[2, i] = TrajectoryGain * RightHipDatalist[BufferCounter * TrajectorySpeed];
                            _buffer[3, i] = TrajectoryGain * RightKneeDatalist[BufferCounter * TrajectorySpeed];
                            BufferCounter = BufferCounter + TrajectorySpeed;
                        }
                        BufferPos2 = _buffer;
                    }

                    if (BufferStatus == 2) // finish process buffer1 on beckhoff
                    {
                        for (int i = 0; i < 500; i++)
                        {
                            _buffer[0, i] = TrajectoryGain * leftHipDatalist[BufferCounter * TrajectorySpeed];
                            _buffer[1, i] = TrajectoryGain * leftKneeDatalist[BufferCounter * TrajectorySpeed];
                            _buffer[2, i] = TrajectoryGain * RightHipDatalist[BufferCounter * TrajectorySpeed];
                            _buffer[3, i] = TrajectoryGain * RightKneeDatalist[BufferCounter * TrajectorySpeed];
                            BufferCounter = BufferCounter + TrajectorySpeed;
                        }
                        BufferPos1 = _buffer;
                    }
                }
            }
            else if(e.PropertyName == X.WhichEndOfTraj.ToString())
            {
                if (WhichEndOfTraj == 0)
                {
                    //mytraj.btn_left_traj.IsEnabled = false;
                    //mytraj.btn_right_traj.IsEnabled = false;
                    EnableButtons = 0;
                    Console.WriteLine("start a traj");
                }
                else if (WhichEndOfTraj==1)
                {
                    //end of start trajectroy
                    //mytraj.btn_left_traj.IsEnabled = true;
                    //Trajectory.enableButtonsOnTrajEnd();
                    EnableButtons = 1;
                    Console.WriteLine("end of start trajectroy");
                }
                else if(WhichEndOfTraj==2)
                {
                    //end of left trajectroy
                    //mytraj.btn_right_traj.IsEnabled = true;
                    EnableButtons = 2;
                    Console.WriteLine("end of left trajectroy");
                }
                else if(WhichEndOfTraj==3)
                {
                    //end of right trajectroy
                    //mytraj.btn_left_traj.IsEnabled = true;
                    EnableButtons = 3;
                    Console.WriteLine("end of riight trajectroy");
                }
                else if(WhichEndOfTraj == 4)
                {
                    EnableButtons = 4;
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_connection == null) return;
            _connection.PropertyChanged -= _connection_PropertyChanged;
            _connection.Dispose();
        }

        #endregion

        //#region INotifyPropertyChanged Members

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //#endregion

    }
}
