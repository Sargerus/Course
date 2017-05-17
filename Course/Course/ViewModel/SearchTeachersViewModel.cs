using Course.Model;
using Course.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    public class SearchTeachersViewModel : ViewModelBase
    {
        private string booknumber;
        private string lname;

        public class Teachers
        {
            public string Номер_трудовой_книжки { get; set; }
            public string Фамилия_И_О_ { get; set; }
            public string Кафедра { get; set; }
            public string Кабинет { get; set; }
            public string Предметы { get; set; }

            public Teachers(string number, string fio,
                          string pulpit, string cabinet, string subjects)
            {
                Номер_трудовой_книжки = number;
                Фамилия_И_О_ = fio;
                Кафедра = pulpit;
                Кабинет = cabinet;
                Предметы = subjects;
            }

            public override string ToString()
            {
                return " Номер трудовой книжки: " + Номер_трудовой_книжки + "\r\n" +
                 "Фамилия И О: " + Фамилия_И_О_ + "\r\n" +
                 "Кафедра: " + Кафедра + "\r\n" +
                 "Кабинет: " + Кабинет + "\r\n" +
                 "Предметы: " + Предметы + "\r\n";
            }
        }
        public string BookNumber
        { 
            get { return booknumber; }
            set
            {
                booknumber = value;

                if (buf == null)
                {
                    RefreshDatabase();
                    return;
                }

                if (booknumber != string.Empty && booknumber != null)
                    buf = (from g in buf where g.Номер_трудовой_книжки.Contains(value) select g).ToList();
                else return;
            }
        }
        public string LName
        {
            get { return lname; }
            set
            {
                lname = value;

                if (buf == null)
                {
                    RefreshDatabase();
                    return;
                }

                if (lname != string.Empty && lname != null)
                    buf = (from g in buf where g.Фамилия_И_О_.Contains(value) select g).ToList();
                else return;
            }
        }
        List<Teachers> buf;
        private List<Teachers> Total { get; set; }
        public List<Teachers> mainlist { get; set; }
        public GeneralCommand BackCommand { get; set; }
        public GeneralCommand SaveCommand { get; set; }
        public GeneralCommand SearchStudentsCommand { get; set; }

        public SearchTeachersViewModel()
        {
            List<Преподаватели> buffer = (sqlcon.GetTeachers()).ToList();
            SaveCommand = new GeneralCommand(Save, null);
            SearchStudentsCommand = new GeneralCommand(SearchStudents, null);
            BackCommand = new GeneralCommand(Back, null);
            

            mainlist = new List<Teachers>(buffer.Count);
            int k = 0;
            while (k < buffer.Count)
            {
                mainlist.Add(new Teachers(buffer[k].Номер_трудовой_книжки, buffer[k].Фамилия_И_О_,
                                          buffer[k].Кафедра, buffer[k].Кабинет, buffer[k].Предметы));
                k++;
            }
            Total = mainlist;
            buf = null;
        }

        public void Back()
        {
            var NewWindow = new StudentMain();

            NewWindow.Top = Application.Current.MainWindow.Top;
            NewWindow.Left = Application.Current.MainWindow.Left;
            NewWindow.Height = Application.Current.MainWindow.ActualHeight;
            NewWindow.Width = Application.Current.MainWindow.ActualWidth;

            NewWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = NewWindow;
        }

        public void Save()
        {
            try
            {
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.FileName = "*.txt";
                savefiledialog.Filter = "TXT File|*.txt";
                savefiledialog.Title = "Saving result";
                savefiledialog.ShowDialog();

                if (System.IO.File.Exists(savefiledialog.FileName))
                    System.IO.File.Delete(savefiledialog.FileName);

                if (savefiledialog.FileName != "")
                {
                    for (int g = 0; g < mainlist.Count; g++)
                    {
                        System.IO.File.AppendAllText(savefiledialog.FileName, (g + 1).ToString());
                        System.IO.File.AppendAllText(savefiledialog.FileName, mainlist[g].ToString());
                        System.IO.File.AppendAllText(savefiledialog.FileName, "\r\n");
                    }
                }
            }
            catch
            {

            }
        }
        public void SearchStudents()
        {
            var NewWindow = new SearchStudentsWindow();
            NewWindow.Top = Application.Current.MainWindow.Top;
            NewWindow.Left = Application.Current.MainWindow.Left;
            NewWindow.Height = Application.Current.MainWindow.ActualHeight;
            NewWindow.Width = Application.Current.MainWindow.ActualWidth;
            NewWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = NewWindow;
        }
      
        private void RefreshDatabase()
        {
            buf = Total;
            LName = lname;
            BookNumber = booknumber;

            mainlist = buf;
            OnPropertyChanged("mainlist");
            buf = null;

        }
    }
}
