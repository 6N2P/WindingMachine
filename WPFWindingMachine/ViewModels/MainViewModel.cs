﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;
using WorkerWithSerialPort;
using WpfApp1.Views;

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
            IsRotateLeft = true;
            IsRotateRight = true;
            IsEnabledActivCBPorts = true;
            _windingUCvm = new WindingUCViewModel();
            WindingUC = new WindingUserControl(_windingUCvm);
            _cncCommands = new SPWorkerWindingMachine(_serialPort);
            ContentStartButton = "Cтарт";
            _isStartMainMotor = false;
        }


        private WindingUCViewModel _windingUCvm;
        private ICNCCommands _cncCommands;
        private SerialPort _serialPort = new SerialPort();
        private int _stepsFreeMove;
        private bool _isStartMainMotor;
        private string _textCountWinding;

        private string _selectedPort;       

        private List<string> _portsList;
        private string _connectionFlag;
        private bool _isEnabledactivCBPorts;
        private bool _isRotateLeft;
        private bool _isRotateRight;
        private string _contentStartButton;

        #region Propertys

        public string ContentStartButton
        {
            get=> _contentStartButton;
            set
            {
                _contentStartButton = value;
                OnPropertyChanged(nameof(ContentStartButton));
            }
        }
        private WindingUserControl _windingUC;
           public WindingUserControl WindingUC
        {
            get => _windingUC;
            set
            {
                _windingUC = value;
                OnPropertyChanged(nameof(WindingUC));
            }
        }

        public bool IsRotateLeft
        {
            get => _isRotateLeft;
            set
            {
                _isRotateLeft = value;
                OnPropertyChanged(nameof(IsRotateLeft));
            }
        }

        public bool IsRotateRight
        {
            get => _isRotateRight;
            set
            {
                _isRotateRight = value;
                OnPropertyChanged(nameof(IsRotateRight));
            }
        }
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
                   // if (_serialPort != null && _serialPort.IsOpen)
                   // {
                        //string command = $"STEPS_FREE:{StepsFreeMove}\n"; // \n — конец строки для Arduino
                        //_serialPort.Write(command);                        
                   // }
                    _cncCommands.SetStepFreeMuve(StepsFreeMove);
                }));
            }
        }
        private DelegateCommand _muveFreeMotorCommand;
        public DelegateCommand MuveFreeMotorCommand
        {            
            get
            {
                return _muveFreeMotorCommand ?? (_muveFreeMotorCommand = new DelegateCommand(obj =>
                {
                    //if (_serialPort != null && _serialPort.IsOpen)
                    //{
                    //    string command = "MUVE_FREE\n";
                    //    _serialPort.Write(command);
                    //}
                    _cncCommands.MuveFreeMotor();
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
                    if (_serialPort != null && _serialPort.IsOpen)
                    {
                        _cncCommands.SetDirectionMotorRight();
                        IsRotateRight = false;
                        IsRotateLeft = true;
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
                    if (_serialPort != null && _serialPort.IsOpen)
                    {
                      //  _serialPort.Write("F\n");
                      _cncCommands.SetDirectionMotorLeft();
                        IsRotateRight = true;
                        IsRotateLeft = false;
                    }
                }));
            }
        }
        private DelegateCommand _mainMortorCommand;
        public DelegateCommand MainMortorCommand
        {
            get
            {
                return _mainMortorCommand ?? (_mainMortorCommand = new DelegateCommand(obj =>
                {
                    if(!_isStartMainMotor )
                    {
                        ContentStartButton = "Стоп";
                        _isStartMainMotor = true;
                    }
                    else
                    {
                        ContentStartButton = "Старт";
                        _isStartMainMotor = false;
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


                if (WindingUC != null)
                    _windingUCvm.SerialPort = _serialPort;
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
               var portsList = new List<string>();
                foreach (string port in ports)
                {
                    portsList.Add(port);
                }

                PortsList = portsList;
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
