using System.IO.Ports;


namespace WpfApp1.ViewModels
{
    public class WindingUCViewModel : ViewModelBase
    {
        private double _wireDiameter; //диамет провода
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
