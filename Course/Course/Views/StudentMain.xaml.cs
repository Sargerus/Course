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

namespace Course
{
    /// <summary>
    /// Логика взаимодействия для StudentMain.xaml
    /// </summary>
    public partial class StudentMain : Window
    {
        public StudentMain()
        {
            DataContext = new StudentWindowViewModel();
            InitializeComponent();

            Button b = (Button)this.FindName("OnlyForDean");
            if (StudentWindowViewModel.AccesLevel != AccesLevels.Dean)
                b.IsEnabled = false;
            b = (Button)this.FindName("NotForUsers");
            if (StudentWindowViewModel.AccesLevel == AccesLevels.User)
                b.IsEnabled = false;

            
        }
    }
}
