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
        public GeneralCommand BeginCheckCommand { get; set; }
        public MainWindowViewModel()
        {
            //this.Login = "user";
            //this.Password = "password";
            BeginCheckCommand = new GeneralCommand(LogAndPassToDatabase, IsLogAndPassValid);
        }

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

      
        private bool IsLogAndPassValid()
        {
            return !String.IsNullOrWhiteSpace(Login) && !String.IsNullOrWhiteSpace(Password);
        }

        char[] GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            
            char[] newhash = new char[hash.Length+4];
            int i = 0;

            for (; i < 8; i++)
                newhash[i]=hash[i];
            newhash[i]='-';
            i++;

            for (; i < 13; i++)
                newhash[i] = hash[i - 1];
            newhash[i] = '-';

            i++;
            for (; i < 18; i++)
                newhash[i] = hash[i - 2];
            newhash[i] = '-';

            i++;

            for (; i < 23; i++)
                newhash[i] = hash[i - 3];
            newhash[i] = '-';

            i++;

            for (; i < 36; i++)
                newhash[i] = hash[i - 4];

            return newhash;
        }  

        private void LogAndPassToDatabase()
        {
            Entities me = new Entities();

            var s = (from g in me.Users where g.UserName == this.Login select g.Password).ToList();
            
            char[] str = GetHashString(Password);
            bool flag = true;
            for (int i = 0; i < 36; i++)
                if (str[i] != s[0].ToString()[i])
                    flag = false;


            if (flag)
            {

                var NewWindow = new StudentMain();
                NewWindow.Show();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = NewWindow;

            }
            else MessageBox.Show("Неверный пароль");
            //md5

        }
    }
}
