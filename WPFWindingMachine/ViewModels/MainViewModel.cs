using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel() 
        {
            _serialPort = new SerialPort();
            _serialPort.DataReceived += GetData;
            StepsFreeMove = 100;
            ConnectionFlag = "Подключиться";
        }

   

        private SerialPort _serialPort = new SerialPort();
        private int _stepsFreeMove;

        private string _textCountWinding;

        private string _selectedPort;       

        private List<string> _portsList;
        private string _connectionFlag;
        private bool _isEnabledactivCBPorts;

        #region Propertys
        public bool IsEnabledActivCBPorts
        {
            get=>_isEnabledactivCBPorts;
            set
            {
                _isEnabledactivCBPorts = value;
                OnPropertyChanged(nameof(IsEnabledActivCBPorts));
            }
        }
        public string ConnectionFlag
        {
            get => _connectionFlag;
            set
            {
                _connectionFlag = value;
                OnPropertyChanged(nameof(ConnectionFlag));
            }
        }
        public string SelectedPort
        {
            get => _selectedPort;
            set
            {
                _selectedPort = value;
                OnPropertyChanged(nameof(SelectedPort));
            }
        }
        public List<string> PortsList
        {
            get => _portsList;
            set
            {
                _portsList = value;
                OnPropertyChanged(nameof(PortsList));
            }
        }
        public string TextCountWinding
        {
            get => _textCountWinding;
            set
            {
                _textCountWinding = value;
                OnPropertyChanged(nameof(TextCountWinding));
            }
        }
        public int StepsFreeMove
        {
            get => _stepsFreeMove;
            set
            {
                _stepsFreeMove = value;
                OnPropertyChanged(nameof(StepsFreeMove));
            }
        }
        #endregion Propertys

        #region Commands
        private DelegateCommand _getPortsListCommand;
        public DelegateCommand GetPortsListCommand
        {
            get
            {
                return _getPortsListCommand ?? (_getPortsListCommand = new DelegateCommand(obj =>
                {
                    GetPorts();
                }));
            }
        }
        private DelegateCommand _connectionToPortCommand;
        public DelegateCommand ConnectionToPortCommand
        {
            get
            {
                return _connectionToPortCommand ?? (_connectionToPortCommand = new DelegateCommand(obj =>
                {
                    ConnectionOrDisconectionToPort();
                }));
            }
        }
        private DelegateCommand _setStepsFreeMoveCommand;
        public DelegateCommand SetStepsFreeMoveCommand
        {
            get
            {
                return _setStepsFreeMoveCommand ?? (_setStepsFreeMoveCommand = new DelegateCommand(obj =>
                {
                    if (_serialPort.IsOpen)
                    {
                        string command = $"STEPS:{StepsFreeMove}\n"; // \n — конец строки для Arduino
                        _serialPort.Write(command);
                    }
                }));
            }
        }
        private DelegateCommand _set1DirectionMotorCommand;
        public DelegateCommand Set1DirectionMotorCommand
        {
            get
            {
                return _set1DirectionMotorCommand ?? (_set1DirectionMotorCommand = new DelegateCommand(obj =>
                {
                    if (_serialPort.IsOpen)
                    {
                        _serialPort.Write("B\n");
                    }
                }));
            }
        }
        private DelegateCommand _set2DirectionMotorCommand;
        public DelegateCommand Set2DirectionMotorCommand
        {
            get
            {
                return _set2DirectionMotorCommand ?? (_set2DirectionMotorCommand = new DelegateCommand(obj =>
                {
                    if (_serialPort.IsOpen)
                    {
                        _serialPort.Write("F\n");
                    }
                }));
            }
        }

        #endregion Commands


        private void ConnectionOrDisconectionToPort()
        {
            if (ConnectionFlag == "Подключиться")
            {
                ConnectionToPort();

            }
            else if (ConnectionFlag == "Отключиться")
            {
                DisconnectionToPort();
            }
        }

        private void DisconnectionToPort()
        {
            try
            {
                _serialPort.Close();
                IsEnabledActivCBPorts = true;
                ConnectionFlag = "Подключиться";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ConnectionToPort()
        {
            try
            {
                _serialPort.PortName = SelectedPort;
                _serialPort.BaudRate = 9600;
                _serialPort.Open();
                IsEnabledActivCBPorts = false;
                ConnectionFlag = "Отключиться";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            PortsList = null; ;

            if (ports.Length > 0)
            {
                PortsList = new List<string>();
                foreach (string port in ports)
                {
                    PortsList.Add(port);
                }
                SelectedPort = PortsList[0];
            }
        }

        private void GetData(object sender, SerialDataReceivedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                TextCountWinding = _serialPort.ReadLine();
            });
        }
    }
}
