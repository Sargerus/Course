using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    public class AttestationWindowViewModel : ViewModelBase
    {
        public class Student
        {
            public string Номер_студенческого_билета { get; set; }
            public string Фамилия { get; set; }
            public string Факультет { get; set; }
            public float Оценка { get; set; }
            public string Название_предмета { get; set; }
            public Nullable<short> Курс { get; set; }
            public Nullable<short> Группа { get; set; }
     
            public Student()
            {

            }
            public Student(string stud, string lname, string faculty, string subname, float mark, Nullable<short> cour, Nullable<short> group)
            {
                Номер_студенческого_билета = stud;
                Фамилия = lname;
                Факультет = faculty;
                Название_предмета = subname;
                Оценка = mark;
                Курс = cour;
                Группа = group;
            }
            public override string ToString()
            {
                return " Номер студенческого билета: " + Номер_студенческого_билета.ToString() + "\r\n" +
                       "Фамилия: " + Фамилия.ToString() + "\r\n" +
                       "Факультет: " + Факультет.ToString() + "\r\n" +
                       "Название предмета: " + Название_предмета.ToString() + "\r\n" +
                       "Оценка: " + Оценка.ToString() + "\r\n" +
                       "Курс: " + Курс.ToString() + "\r\n" +
                       "Группа: " + Группа.ToString() + "\r\n";
                      
            }


        }

        private List<Student> Total { get; set; }
        public List<Student> mainlist { get; set; }
        public GeneralCommand BeginAttCommand { get; set; }
        public GeneralCommand ClearCommand { get; set; }
        public GeneralCommand BackCommand { get; set; }
        public string StudentFaculty { get; set; }
        public Nullable<short> StudentCourse { get; set; }
        public Nullable<short> StudentGroup { get; set; }

        public AttestationWindowViewModel()
        {
            BeginAttCommand = new GeneralCommand(BeginAtt, null);
            ClearCommand = new GeneralCommand(Clear, null);
            BackCommand = new GeneralCommand(Back, null);
            var JoinedTable = (sqlcon.DBase.Студенты.Join(sqlcon.DBase.Оценки, p => p.Номер_студенческого_билета, c => c.Номер_студенческого_билета,
               (p, c) => new
               {
                   Номер_студбилета = p.Номер_студенческого_билета,
                   Фамилия = p.Фамилия,
                   Факультет = p.Факультет,
                   Курс = p.Курс,
                   Группа = p.Группа,
                   Название_предмета = c.Название_предмета,
                   Оценка = c.Оценка
               })).ToList();

            mainlist = new List<Student>(JoinedTable.Count);

            int k = 0;
            while (k < JoinedTable.Count)
            {
                mainlist.Add(new Student(JoinedTable[k].Номер_студбилета, JoinedTable[k].Фамилия, JoinedTable[k].Факультет,
                                      JoinedTable[k].Название_предмета, (float)JoinedTable[k].Оценка, JoinedTable[k].Курс, JoinedTable[k].Группа));
                k++;
            }
            Total = mainlist;
        }

        public void Clear()
        {
            StudentCourse = null;
            StudentFaculty = String.Empty;
            StudentGroup = null;
            mainlist = Total;

            OnPropertyChanged("StudentCourse");
            OnPropertyChanged("StudentFaculty");
            OnPropertyChanged("StudentGroup");
            OnPropertyChanged("mainlist");
        }

        public void Back()
        {
            var NewWindow = new StudentMain();

            NewWindow.Height = Application.Current.MainWindow.ActualHeight;
            NewWindow.Width = Application.Current.MainWindow.ActualWidth;

            NewWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = NewWindow;
        }
        public void BeginAtt()
        {
            mainlist = Total;
            mainlist = (mainlist.Select(g => g).Where(g => g.Факультет == StudentFaculty && g.Курс == StudentCourse && g.Группа == StudentGroup).OrderBy(g => g.Фамилия)).ToList();
            OnPropertyChanged("mainlist");
        }

       
    }
}
