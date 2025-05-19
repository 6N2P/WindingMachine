using System.IO.Ports;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для WindingUserControl.xaml
    /// </summary>
    public partial class WindingUserControl : UserControl
    {
        public WindingUserControl()
        {
            InitializeComponent();
            _vm = new WindingUCViewModel();
            _vm.SerialPort = SerialPort;
            DataContext =_vm ;
        }

        private WindingUCViewModel _vm;
        public SerialPort SerialPort { get; set; }
    }
}
