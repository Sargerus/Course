using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Model;
using System.Windows;
using Course.Views;
using Microsoft.Win32;

namespace Course.ViewModel
{

    public class TeacherWindowViewModel : ViewModelBase
    {
        public class Teachers
        {
            public string Номер_трудовой_книжки { get; set; }
            public string Фамилия_И_О_ { get; set; }
            public string Кафедра { get; set; }
            public string Кабинет { get; set; }
            public string Предметы { get; set; }

            public Teachers()
            {

            }
            public Teachers(string number,string fio,
                           string pulpit,string cabinet,string subjects)
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

        public List<Преподаватели> s { get; set; }
        public List<Teachers> teachers { get; set; }
        public GeneralCommand ShowStudentCommand { get; set; }
        public GeneralCommand ShowPerfomanceCommand { get; set; }
        public GeneralCommand SaveCommand { get; set; }
        public GeneralCommand SearchStudentsCommand { get; set; }
        public GeneralCommand SearchTeachersCommand { get; set; }

        public TeacherWindowViewModel()
        {
            ShowStudentCommand = new GeneralCommand(ShowStudents, null);
            ShowPerfomanceCommand = new GeneralCommand(ShowPerfomance, null);
            SearchStudentsCommand = new GeneralCommand(SearchStudents, null);
            SearchTeachersCommand = new GeneralCommand(SearchTeacher, null);
            SaveCommand = new GeneralCommand(Save, null);
            ShowTable();

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
                    for (int g = 0; g < teachers.Count; g++)
                    {
                        System.IO.File.AppendAllText(savefiledialog.FileName, (g + 1).ToString());
                        System.IO.File.AppendAllText(savefiledialog.FileName, teachers[g].ToString());
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
        public void SearchTeacher()
        {
            var NewWindow = new SearchTeachers();

            NewWindow.Top = Application.Current.MainWindow.Top;
            NewWindow.Left = Application.Current.MainWindow.Left;
            NewWindow.Height = Application.Current.MainWindow.ActualHeight;
            NewWindow.Width = Application.Current.MainWindow.ActualWidth;
         
            NewWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = NewWindow;
        }
        public void ShowStudents()
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
        public void ShowPerfomance()
        {
            var NewWindow = new PerfomanceWindow();

            NewWindow.Top = Application.Current.MainWindow.Top;
            NewWindow.Left = Application.Current.MainWindow.Left;
            NewWindow.Height = Application.Current.MainWindow.ActualHeight;
            NewWindow.Width = Application.Current.MainWindow.ActualWidth;
         
            
            NewWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = NewWindow;
        }
        private void ShowTable()
        {
            s = (sqlcon.GetTeachers()).ToList();

            teachers = new List<Teachers>(s.Count);
            int k = 0;
            while (k < s.Count)
            {
                teachers.Add(new Teachers(s[k].Номер_трудовой_книжки, s[k].Фамилия_И_О_,
                                          s[k].Кафедра, s[k].Кабинет, s[k].Предметы));
                k++;
            }
        }
    }
}
