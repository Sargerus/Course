using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Course.Model;
using Course.Views;
using Microsoft.Win32;

namespace Course.ViewModel
{
    public class StudentWindowViewModel : ViewModelBase
    {
        public class Student
        {
            public string Номер_студенческого_билета { get; set; }
            public string Фамилия { get; set; }
            public string Факультет { get; set; }
            public string Специальность { get; set; }
            public Nullable<short> Курс { get; set; }
            public Nullable<short> Группа { get; set; }
            public Nullable<short> Подгруппа { get; set; }
            public Student()
            {

            }
            public Student(string stud, string lname, string faculty, string prof, Nullable<short> cour, Nullable<short> group, Nullable<short> subgroup)
            {
                Номер_студенческого_билета = stud;
                Фамилия = lname;
                Факультет = faculty;
                Специальность = prof;
                Курс = cour;
                Группа = group;
                Подгруппа = subgroup;
            }
            public override string ToString()
            {
                return "Номер студенческого билета: " + Номер_студенческого_билета.ToString() + " " +
                       "Фамилия: " + Фамилия.ToString() + " " +
                       "Факультет: " + Факультет.ToString() + " " + 
                       "Специальность: " + Специальность.ToString() + " " + 
                       "Курс: " + Курс.ToString() + " " + 
                       "Группа: " + Группа.ToString() + " " + 
                       "Подгруппа: " + Подгруппа.ToString();
            }
            
        }

        public List<Студенты> s { get; set; }
        public List<Student> student { get; set; }
        public GeneralCommand ShowTeachersCommand { get; set; }
        public GeneralCommand ShowPerfomanceCommand { get; set; }
        public GeneralCommand SaveCommand { get; set; }
        public GeneralCommand SearchStudentsCommand { get; set; }
        public StudentWindowViewModel()
        {
            
            ShowTeachersCommand = new GeneralCommand(ShowTeachers, null);
            ShowPerfomanceCommand = new GeneralCommand(ShowPerfomance, null);
            SaveCommand = new GeneralCommand(Save, null);
            SearchStudentsCommand = new GeneralCommand(SearchStudents, null);
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
                    for (int g = 0; g < student.Count; g++)
                    {
                        System.IO.File.AppendAllText(savefiledialog.FileName, student[g].ToString());
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
            NewWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = NewWindow;
        }
        public void ShowTeachers()
        {
            var NewWindow = new TeachersWindow();
            NewWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = NewWindow;
        }
        public void ShowPerfomance()
        {
            var NewWindow = new PerfomanceWindow();
            NewWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = NewWindow;
        }


        private void ShowTable()
        {
            //logic work with data for DataGrid
            s = (sqlcon.GetStudents()).ToList();
            student = new List<Student>(s.Count);
            int k = 0;
            while (k < s.Count)
            {
                student.Add(new Student(s[k].Номер_студенческого_билета, s[k].Фамилия, s[k].Факультет,
                                     s[k].Специальность, s[k].Курс, s[k].Группа, s[k].Подгруппа));
                k++;
            }
            
        }
    }
}
