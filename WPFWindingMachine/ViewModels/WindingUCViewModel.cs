using System.Collections.ObjectModel;
using System.IO.Ports;
using WindingModels;


namespace WpfApp1.ViewModels
{
    public class WindingUCViewModel : ViewModelBase
    {
        public WindingUCViewModel()
        {
            WireDiameter = 0.08;
            InnerDiametrTor = 50;
            KoefStep = 1;
            _stepsCalculate = new StepsCalculateTor(WireDiameter,800,InnerDiametrTor);
            
        }


        private StepsCalculateBase _stepsCalculate;
        private double _wireDiameter; //диамет провода
        private double _innerDiametrTor;
        private int _windingStep;
        private double _koefStep;
        private int _corentStep;
        private ObservableCollection<WindingData> _windingList;

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
                _innerDiametrTor = value;
                OnPropertyChanged(nameof(InnerDiametrTor));
            }
        }
        public double WireDiameter
        {
            get => _wireDiameter;
            set
            {
                _wireDiameter = value;
                OnPropertyChanged(nameof(WireDiameter));
            }
        }

       public SerialPort SerialPort { get; set; }



    }
}
