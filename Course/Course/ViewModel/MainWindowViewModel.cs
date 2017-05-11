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
            _login = "Dean";
            _password = "dean";
            
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

        string GetHashString(string s)
        {
            MD5 md5Hasher = MD5.Create();

            // Преобразуем входную строку в массив байт и вычисляем хэш
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));

            // Создаем новый Stringbuilder (Изменяемую строку) для набора байт
            StringBuilder sBuilder = new StringBuilder();

            // Преобразуем каждый байт хэша в шестнадцатеричную строку
            for (int i = 0; i < data.Length; i++)
            {
                //указывает, что нужно преобразовать элемент в шестнадцатиричную строку длиной в два символа
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
            
        }  

        private void LogAndPassToDatabase()
        {
            
            Entities me = new Entities();
            me.ChangeTracker.DetectChanges();

            var s = (from g in me.Users where g.UserName == this.Login select g).ToList();
            
            string str = GetHashString(Password);
            bool flag = false;
            
            if (str.Equals(s[0].Password.ToString().Replace("-",String.Empty)))
                flag = true;
            if (flag)
            {
                switch(s[0].Acceslevel)
                {
                    case 1: AccesLevel = AccesLevels.User; break;
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
