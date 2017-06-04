using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Model;
using System.Globalization;
using System.Windows;

namespace Course.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public static AccesLevels AccesLevel;
        public static string StudNumber;
        public SqlConnection sqlcon;
        public event PropertyChangedEventHandler PropertyChanged;

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                
                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/lang.en-US.xaml", UriKind.Relative);
                        break;
                }

               
                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }
                Course.Properties.Settings.Default.DefaultLanguage = Language;
                Course.Properties.Settings.Default.Save();
            }
        }


        public void OnPropertyChanged(string str)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(str));
        }
        public ViewModelBase()
        {
           sqlcon = new SqlConnection();
           Language = Course.Properties.Settings.Default.DefaultLanguage;
        }
    }
}
