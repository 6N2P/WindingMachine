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
        public WindingUserControl(WindingUCViewModel _vm)
        {
            InitializeComponent();            
            DataContext =_vm ;
        }

    }
}
