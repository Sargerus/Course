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
        public void ChangeLangRus()
        {
            Language = new System.Globalization.CultureInfo("ru-RU");
        }
        public void ChangeLangEng()
        {
            Language = new System.Globalization.CultureInfo("en-US");

        }
        public GeneralCommand ChangeLangRusCommand { get; set; }
        public GeneralCommand ChangeLangEngCommand { get; set; }
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
            ConnectCommands();
            ShowTable();

        }

        private void ConnectCommands()
        {
            ShowStudentCommand = new GeneralCommand(ShowStudents, null);
            ShowPerfomanceCommand = new GeneralCommand(ShowPerfomance, null);
            SearchStudentsCommand = new GeneralCommand(SearchStudents, null);
            SearchTeachersCommand = new GeneralCommand(SearchTeacher, null);
            SaveCommand = new GeneralCommand(Save, null);
            ChangeLangEngCommand = new GeneralCommand(ChangeLangEng, null);
            ChangeLangRusCommand = new GeneralCommand(ChangeLangRus, null);
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

            var table = (from a in sqlcon.DBase.Преподаватели
                        from b in a.Предметы
                        join c in sqlcon.DBase.Предметы on b.Название_предмета equals c.Название_предмета  
                        select new  
                        {
                        Numb = a.Номер_трудовой_книжки,
                        Fam = a.Фамилия_И_О_, 
                        Kaf = a.Кафедра, 
                        Kab = a.Кабинет,
                        Naz = c.Название_предмета
                        }).ToList();

            teachers = new List<Teachers>(table.Count());

            int k = 0;
              teachers.Add(new Teachers(table[k].Numb, table[k].Fam,
                                         table[k].Kaf, table[k].Kab, table[k].Naz));
              k++;

              var z = table[0];
            while (k < table.Count())
            {
                if (table[k].Fam.Equals(z.Fam))
                    teachers.Add(new Teachers(null, null,
                                             null, null, table[k].Naz));
                else
                {
                    teachers.Add(new Teachers(table[k].Numb, table[k].Fam,
                                       table[k].Kaf, table[k].Kab, table[k].Naz));
                    z = table[k];
                }
              
                k++;
            }
   
        }
    }
}
