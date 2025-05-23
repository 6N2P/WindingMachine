using System;
using System.Collections.ObjectModel;
using System.IO.Ports;
using WindingModels;
using WorkerWithSerialPort;


namespace WpfApp1.ViewModels
{
    public class WindingUCViewModel : ViewModelBase
    {
        public WindingUCViewModel()
        {
            WireDiameter = 0.08;
            InnerDiametrTor = 50;
            OuterDiametrTor = 78.5;
            DiameterOfDriveRoller = 32.5;
            KoefStep = 1;

            CountTurns = 2;
            _cncCommands = new SPWorkerWindingMachine(SerialPort);
        }


        #region Fields
        private ICalculateSteps _stepsCalculate;
        private double _wireDiameter; //диамет провода
        private double _innerDiametrTor;
        private int _windingStep;
        private double _koefStep;
        private int _corentStep;
        private ObservableCollection<WindingData> _windingList;
        private int _countTurns;
        private double _outerDiametrTor;
        private double _diameterOfDriveRoller;
        private ICNCCommands _cncCommands;
        #endregion

        #region Propertys
        public double DiameterOfDriveRoller
        {
            get => _diameterOfDriveRoller;
            set
            {
                _diameterOfDriveRoller = value;
                OnPropertyChanged(nameof(DiameterOfDriveRoller));
            }
        }
        public double OuterDiametrTor
        {
            get => _outerDiametrTor;
            set
            {
                _outerDiametrTor = value;
                OnPropertyChanged(nameof(OuterDiametrTor));
            }
        }
        public int CountTurns
        {
            get => _countTurns;
            set
            {
                _countTurns = value;
                OnPropertyChanged(nameof(CountTurns));
            }
        }
        private ObservableCollection<WindingData> WindingList
        {
            get => _windingList;
            set
            {
                _windingList = value;
                OnPropertyChanged(nameof(WindingList));
            }
        }
        public int CorentStep
        {
            get => _corentStep;
            set
            {
                _corentStep = value;
                OnPropertyChanged(nameof(CorentStep));
            }
        }
        public double KoefStep
        {
            get => _koefStep;
            set
            {
                _koefStep = value;
                OnPropertyChanged(nameof(KoefStep));
            }
        }
        public int WindingStep
        {
            get => _windingStep;
            set
            {
                _windingStep = value;
                OnPropertyChanged(nameof(WindingStep));
            }
        }
        public double InnerDiametrTor
        {
            get => _innerDiametrTor;
            set
            {
                if (value != 0)
                {
                    _innerDiametrTor = value;
                    OnPropertyChanged(nameof(InnerDiametrTor));
                }
                else _innerDiametrTor = 20;
            }
        }
        public double WireDiameter
        {
            get => _wireDiameter;
            set
            {
                if (value != 0)
                {
                    _wireDiameter = value;
                    OnPropertyChanged(nameof(WireDiameter));
                }
                else _wireDiameter = 0.08;
            }
        }

        public SerialPort SerialPort { get; set; } 
        #endregion Propertys

        #region Commands
        private DelegateCommand _calcStepCommand;
        public DelegateCommand CalcStepCommand
        {
            get
            {
                return _calcStepCommand ?? (_calcStepCommand = new DelegateCommand(obj =>
                {
                    CalculateStep();
                }));
            }
        }
        private DelegateCommand _calcStepByTurnsCommand;
        public DelegateCommand CalcStepByTurnsCommand
        {
            get
            {
                return _calcStepByTurnsCommand ?? (_calcStepByTurnsCommand = new DelegateCommand(obj =>
                {
                    CalculateStepByTurns();
                }));
            }
        }

        private DelegateCommand _setWorkStepCommand;
        public DelegateCommand SetWorkStepCommand
        {
            get
            {
                return _setWorkStepCommand ?? (_setWorkStepCommand = new DelegateCommand(obj =>
                {
                    if (WindingStep != 0 && SerialPort != null)
                    {
                        SetWorkStep();
                        CorentStep = WindingStep;
                    }
                }));
            }
        }
        #endregion Commands

        private void SetWorkStep()
        {
            //if (SerialPort != null && SerialPort.IsOpen)
            //{
            //    string command = $"STEPS:{WindingStep}\n"; // \n — конец строки для Arduino
            //    SerialPort.Write(command);
            //}
            _cncCommands.SetStepWorkMuve(WindingStep);
        }

        private void CalculateStep()
        {
            _stepsCalculate = new StepsCalculateTor(WireDiameter, 3200, InnerDiametrTor,OuterDiametrTor,DiameterOfDriveRoller);
            var s = _stepsCalculate.CalculateSteps();
            var k = s * KoefStep;
            WindingStep = Convert.ToInt32(Math.Round(k, 0));
        }

        private void CalculateStepByTurns()
        {
            _stepsCalculate = new StepsCalculateTor(WireDiameter, 3200, InnerDiametrTor, OuterDiametrTor, DiameterOfDriveRoller);
            var s = _stepsCalculate.CalculateStepsByTurns(CountTurns);
          
            WindingStep = s;
        }
    }
}
