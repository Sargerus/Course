using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ViewModel
{
    public class Slider : INotifyPropertyChanged
    {
        private int currentvalue;
        public int CurrentValue
        {
            get
            {
                return currentvalue;
            }
            set
            {
                currentvalue = value;
                NotifyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
