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
        
        private PLCConnection _connection;
        private UInt32 _gui_manager;
        private float _position_mode_right_hip;
        private float _position_mode_right_knee;
        private float _position_mode_left_hip;
        private float _position_mode_left_knee;
        private UInt32 _buffer_status;
        private int _start_traj_len,_right_traj_len,_left_traj_len;
        private bool _read_actual_pos;
        private Int32 _target;
        private Int32 _target2;
        private Int32 _target3;
        private Int32 _target4;

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
        public int StartTrajLen
        {
            get
            {
                _start_traj_len = (int)_connection[X.StartTrajLen];
                return _start_traj_len;
            }
            set
            {
                _start_traj_len = value;
                _connection[X.StartTrajLen] = _start_traj_len;
            }
        }
        public int RightTrajLen
        {
            get
            {
                _right_traj_len = (int)_connection[X.RightTrajLen];
                return _right_traj_len;
            }
            set
            {
                _right_traj_len = value;
                _connection[X.RightTrajLen] = _right_traj_len;
            }
        }

        public int LeftTrajLen
        {
            get
            {
                _left_traj_len = (int)_connection[X.LeftTrajLen];
                return _left_traj_len;
            }
            set
            {
                _left_traj_len = value;
                _connection[X.LeftTrajLen] = _left_traj_len;
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

        public void sendStartTrajFirstBuffer()
        {
            float[,] _buffer = new float[4, 500];
            float[,] _buffer2 = new float[4, 500];
            for (int i = 0; i < 500; i++)
            {
                _buffer[0, i] = leftHipDatalist[i];
                _buffer[1, i] = leftKneeDatalist[i];
                _buffer[2, i] = RightHipDatalist[i];
                _buffer[3, i] = RightKneeDatalist[i];
                _buffer2[0, i] = leftHipDatalist[i+500];
                _buffer2[1, i] = leftKneeDatalist[i+500];
                _buffer2[2, i] = RightHipDatalist[i+500];
                _buffer2[3, i] = RightKneeDatalist[i+500];
            }
            BufferPos1 = _buffer;
            BufferPos2 = _buffer2;
            BufferCounter = BufferCounter + 1000;
        }

        public void sendRightTrajFirstBuffer()
        {
            float[,] _buffer = new float[4, 500];
            float[,] _buffer2 = new float[4, 500];
            BufferCounter = _start_traj_len;
            int j = _start_traj_len;
            for (int i = 0; i < 500; i++)
            {
                _buffer[0, i] = leftHipDatalist[j];
                _buffer[1, i] = leftKneeDatalist[j];
                _buffer[2, i] = RightHipDatalist[j];
                _buffer[3, i] = RightKneeDatalist[j];
                _buffer2[0, i] = leftHipDatalist[j + 500];
                _buffer2[1, i] = leftKneeDatalist[j + 500];
                _buffer2[2, i] = RightHipDatalist[j + 500];
                _buffer2[3, i] = RightKneeDatalist[j + 500];
                j++;
            }
            BufferPos1 = _buffer;
            BufferPos2 = _buffer2;
            BufferCounter = BufferCounter + 1000;
        }

        public void sendLeftTrajFirstBuffer()
        {
            float[,] _buffer = new float[4, 500];
            float[,] _buffer2 = new float[4, 500];
            BufferCounter = _start_traj_len+_right_traj_len;
            int j = _start_traj_len+_right_traj_len;
            for (int i = 0; i < 500; i++)
            {
                _buffer[0, i] = leftHipDatalist[j];
                _buffer[1, i] = leftKneeDatalist[j];
                _buffer[2, i] = RightHipDatalist[j];
                _buffer[3, i] = RightKneeDatalist[j];
                _buffer2[0, i] = leftHipDatalist[j + 500];
                _buffer2[1, i] = leftKneeDatalist[j + 500];
                _buffer2[2, i] = RightHipDatalist[j + 500];
                _buffer2[3, i] = RightKneeDatalist[j + 500];
                j++;
            }
            BufferPos1 = _buffer;
            BufferPos2 = _buffer2;
            BufferCounter = BufferCounter + 1000;
        }

        private void CommonInitialize()
        {
            _connection.PropertyChanged += _connection_PropertyChanged;
            _connection.Strat();
        }

        private void _connection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == X.BufferStatus.ToString())
            {
                float[,] _buffer = new float[4, 500];
                if (BufferStatus == 1) // finish process buffer2 on beckhoff
                {
                    for (int i = 0; i < 500; i++)
                    {
                        _buffer[0, i] = leftHipDatalist[BufferCounter];
                        _buffer[1, i] = leftKneeDatalist[BufferCounter];
                        _buffer[2, i] = RightHipDatalist[BufferCounter];
                        _buffer[3, i] = RightKneeDatalist[BufferCounter];
                        BufferCounter++;
                    }
                    BufferPos2 = _buffer;
                }

                if (BufferStatus == 2) // finish process buffer1 on beckhoff
                {
                    for (int i = 0; i < 500; i++)
                    {
                        _buffer[0, i] = leftHipDatalist[BufferCounter];
                        _buffer[1, i] = leftKneeDatalist[BufferCounter];
                        _buffer[2, i] = RightHipDatalist[BufferCounter];
                        _buffer[3, i] = RightKneeDatalist[BufferCounter];
                        BufferCounter++;
                    }
                    BufferPos1 = _buffer;
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
