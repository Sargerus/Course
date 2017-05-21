using Course.Model;
using Course.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Course.Views
{
    /// <summary>
    /// Логика взаимодействия для AttestationWindow.xaml
    /// </summary>
    public partial class AttestationWindow : Window
    {
        public AttestationWindow()
        {
            DataContext = new AttestationWindowViewModel();
            InitializeComponent();

            Button b = (Button)this.FindName("Searchb");
            if (StudentWindowViewModel.AccesLevel == AccesLevels.User)
                b.IsEnabled = false;
            b = (Button)this.FindName("Clearb");
            if (StudentWindowViewModel.AccesLevel == AccesLevels.User)
                b.IsEnabled = false;
        }
    }
}
