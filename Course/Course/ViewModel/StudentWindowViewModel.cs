using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Course.Model;
using Course.Views;

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
            
        }

        public List<Студенты> s { get; set; }
        public List<Student> student { get; set; }
        public GeneralCommand ShowTeachersCommand { get; set; }
        public GeneralCommand ShowPerfomanceCommand { get; set; }
        public StudentWindowViewModel()
        {
            
            ShowTeachersCommand = new GeneralCommand(ShowTeachers, null);
            ShowPerfomanceCommand = new GeneralCommand(ShowPerfomance, null);
            ShowTable();
            
        }


        public void ShowTeachers()
        {
            var NewWindow = new TeachersWindow();
            NewWindow.Show();
            Application.Current.MainWindow.Close();
        }
        public void ShowPerfomance()
        {
            var NewWindow = new PerfomanceWindow();
            NewWindow.Show();
            Application.Current.MainWindow.Close();
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
