using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;
using Course.Model;

namespace Course.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _login;
        private string _password;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }


        public GeneralCommand BeginCheckCommand { get; set; }

        public MainWindowViewModel()
        {
            
            _login = "Dean";
            _password = "dean";

            Language = Course.Properties.Settings.Default.DefaultLanguage;
            ConnectCommands();
           
        }
       
        
        private void ConnectCommands()
        {
            BeginCheckCommand = new GeneralCommand(LogAndPassToDatabase, IsLogAndPassValid);
        }
        private bool IsLogAndPassValid()
        {
            return !String.IsNullOrWhiteSpace(Login) && !String.IsNullOrWhiteSpace(Password);
        }

        string GetHashString(string s)
        {
            MD5 md5Hasher = MD5.Create();

           
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));

            
            StringBuilder sBuilder = new StringBuilder();

            
            for (int i = 0; i < data.Length; i++)
            {
               
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
            
        }  

        private void LogAndPassToDatabase()
        {
            
           
            sqlcon.DBase.ChangeTracker.DetectChanges();

            var s = (from g in sqlcon.DBase.Users where g.UserName == this.Login select g).ToList();
            
            string str = GetHashString(Password);
            bool flag = false;
            
            if (str.Equals(s[0].Password.ToString().Replace("-",String.Empty)))
                flag = true;
            if (flag)
            {
                switch(s[0].Acceslevel)
                {
                    case 1: { AccesLevel = AccesLevels.User; StudNumber = _login; } break;
                    case 2: AccesLevel = AccesLevels.Teacher; break;
                    case 3: AccesLevel = AccesLevels.Dean; break;
                    default: MessageBox.Show("Error in database"); Application.Current.MainWindow.Close(); break;
                }

                var NewWindow = new StudentMain();
                NewWindow.Show();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = NewWindow;

            }
            else MessageBox.Show("Неверный пароль");
            

        }
    }
}
