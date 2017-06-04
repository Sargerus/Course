using Course.Model;
using Course.Views;
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
        public class Student:IComparable
        {
            public string Номер_студенческого_билета { get; set; }
            public string Фамилия { get; set; }
            public string Факультет { get; set; }
            public Nullable<short> Курс { get; set; }
            public Nullable<short> Группа { get; set; }
            public float Оценка { get; set; }
            public string Название_предмета { get; set; }
     
            public Student()
            {

            }
            public Student(string stud, string lname, string faculty, Nullable<short> cour, Nullable<short> group,string subname, float mark)
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



            public int CompareTo(object obj)
            {
                Student x = obj as Student;
                return this.Фамилия.CompareTo(x.Фамилия);
                 
            }
        }

        private List<Student> Total { get; set; }
        public List<Student> mainlist { get; set; }
        public GeneralCommand BeginAttCommand { get; set; }
        public GeneralCommand ClearCommand { get; set; }
        public GeneralCommand BackCommand { get; set; }
        public GeneralCommand SearchStudentsCommand { get; set; }
        public GeneralCommand SearchTeachersCommand { get; set; }
        public GeneralCommand AddMarkCommand { get; set; }
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

       
        public string StudentFaculty { get; set; }
        public Nullable<short> StudentCourse { get; set; }
        public Nullable<short> StudentGroup { get; set; }

        public AttestationWindowViewModel()
        {

            ConnectCommands();
            CreateTable();

           
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
        private void ConnectCommands()
        {
            BeginAttCommand = new GeneralCommand(BeginAtt, null);
            ClearCommand = new GeneralCommand(Clear, null);
            BackCommand = new GeneralCommand(Back, null);
            SearchStudentsCommand = new GeneralCommand(SearchStudents, null);
            SearchTeachersCommand = new GeneralCommand(SearchTeacher, null);
            ChangeLangEngCommand = new GeneralCommand(ChangeLangEng, null);
            ChangeLangRusCommand = new GeneralCommand(ChangeLangRus, null);
            AddMarkCommand = new GeneralCommand(AddMark, null);
        }
        private void AddMark()
        {
            var NewWindow = new AddMarkWindow();
            NewWindow.Top = Application.Current.MainWindow.Top;
            NewWindow.Left = Application.Current.MainWindow.Left;
            NewWindow.Show();

        }
        private void CreateTable()
        {
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
            
            if (AccesLevel == AccesLevels.User)
            {

                var buf = JoinedTable.Select(g => g).Where(g => g.Номер_студбилета == StudNumber).ToList();

                if (buf.Count > 0)
                    mainlist.Add(new Student(buf[0].Номер_студбилета, buf[0].Фамилия, buf[0].Факультет,
                                           buf[0].Курс, buf[0].Группа, buf[0].Название_предмета, (float)buf[0].Оценка));

                k++;

                while (k != buf.Count())
                {
                    mainlist.Add(new Student(null, null, null,
                                           null, null, buf[k].Название_предмета, (float)buf[k].Оценка));
                    k++;

                }

            }
            else 
                while (k < JoinedTable.Count) {
 
                    mainlist.Add(new Student(JoinedTable[k].Номер_студбилета, JoinedTable[k].Фамилия, JoinedTable[k].Факультет,
                                           JoinedTable[k].Курс, JoinedTable[k].Группа, JoinedTable[k].Название_предмета, (float)JoinedTable[k].Оценка));
                    k++;
                }

            mainlist.Sort();
            Student z = mainlist[0];
            for (int i = 1; i < mainlist.Count; i++)
            {
                if (mainlist[i].Номер_студенческого_билета.Equals(z.Номер_студенческого_билета))
                {
                    mainlist[i].Номер_студенческого_билета = null;
                    mainlist[i].Фамилия = null;
                    mainlist[i].Факультет = null;
                    mainlist[i].Курс = null;
                    mainlist[i].Группа = null;
                }
                else z = mainlist[i];
            }
            Total = mainlist;
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
        public void BeginAtt()
        {
            mainlist = Total;
            mainlist = (mainlist.Select(g => g).Where(g => g.Факультет == StudentFaculty && g.Курс == StudentCourse && g.Группа == StudentGroup).OrderBy(g => g.Фамилия)).ToList();

            for (int i = 1; i < mainlist.Count; i++)
            {
                if (mainlist[i].Номер_студенческого_билета == mainlist[i - 1].Номер_студенческого_билета)
                {
                    mainlist[i].Группа                                       = null;
                    mainlist[i].Курс                                         = null;
                    mainlist[i].Номер_студенческого_билета                   = string.Empty;
                    mainlist[i].Факультет                                    = string.Empty; 
                    mainlist[i].Фамилия                                      = string.Empty;
                }
            }
            OnPropertyChanged("mainlist");
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
    
    }
}
