using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Model;

namespace Course.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public SqlConnection sqlcon;
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string str)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(str));
        }
        public ViewModelBase()
        {
           sqlcon = new SqlConnection();
        }
    }
}
