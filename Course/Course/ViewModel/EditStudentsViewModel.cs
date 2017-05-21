using Course.Model;
using Course.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Course.ViewModel
{
    public class EditStudentsViewModel : ViewModelBase
    {
        private string lname;
        private string studnumber;
        private int currentvalue;
        private string selectedType;
       
     
        List<Student> buf;


        public GeneralCommand SearchStudentsCommand { get; set; }
        public GeneralCommand SearchTeachersCommand { get; set; }
        public GeneralCommand ResetSliderCommand { get; set; }
        public GeneralCommand BackCommand { get; set; }


        private List<Student> Total { get; set; }
        public List<Student> mainlist { get; set; }


        public ICollection<string> StudentsType { get; set; }


        public class Student
        {
            public string Номер_студенческого_билета { get; set; }
            public string Фамилия { get; set; }
            public string Факультет { get; set; }
            public string Специальность { get; set; }
            public Nullable<short> Курс { get; set; }
            public Nullable<short> Группа { get; set; }
            public Nullable<short> Количество_пропусков_за_всё_время { get; set; }
            public Nullable<double> Средняя_оценка_за_всё_время { get; set; }
            public Student()
            {

            }
            public Student(string stud, string lname, string faculty, string prof, Nullable<short> cour, Nullable<short> group, Nullable<short> miss, Nullable<double> mark)
            {
                Номер_студенческого_билета = stud;
                Фамилия = lname;
                Факультет = faculty;
                Специальность = prof;
                Курс = cour;
                Группа = group;
                Количество_пропусков_за_всё_время = miss;
                Средняя_оценка_за_всё_время = mark;
            }
            public override string ToString()
            {
                return "Номер студенческого билета: " + Номер_студенческого_билета.ToString() + " " +
                       "Фамилия: " + Фамилия.ToString() + " " +
                       "Факультет: " + Факультет.ToString() + " " +
                       "Специальность: " + Специальность.ToString() + " " +
                       "Курс: " + Курс.ToString() + " " +
                       "Группа: " + Группа.ToString() + " " +
                       "Подгруппа: " + Количество_пропусков_за_всё_время.ToString();
            }





        }
        

        public int CurrentValue
        {
            get
            {
                return currentvalue;
            }
            set
            {
                currentvalue = value;
                
                if (buf == null)
                {
                    OnPropertyChanged("CurrentValue");
                    RefreshDatabase();
                    return;
                }

                if (currentvalue != 0)
                    buf = (from g in buf where g.Количество_пропусков_за_всё_время < value select g).ToList();
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
                    buf = (from g in buf where g.Фамилия.Contains(value) select g).ToList();
                else return;
                
            } 
        }
        public string StudNumber {
            get { return studnumber; }
            set
            {
                studnumber = value;

                if (buf == null)
                {
                    RefreshDatabase();
                    return;
                }

                if (studnumber != string.Empty && studnumber != null)
                    buf = (from g in buf where g.Номер_студенческого_билета.Contains(value) select g).ToList();
                else return;
            }
        }
        
        

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //IsChange = true;
        }
    
        
        public EditStudentsViewModel()
        {

            ConnectCommands();
            CreateTable();
              
            StudentsType = new Collection<string>
                                    { 
                                        "Все",
                                        "Отличники", 
                                        "Неотличники", 
                                    };
            SelectedType = StudentsType.First();

        }
      
        
        private void ConnectCommands()
        {
            ResetSliderCommand = new GeneralCommand(ResetSlider);
            BackCommand = new GeneralCommand(Back, null);
            SearchStudentsCommand = new GeneralCommand(SearchStudents, null);
            SearchTeachersCommand = new GeneralCommand(SearchTeacher, null);
            
        }
        private void CreateTable()
        {
            var JoinedTable = (sqlcon.DBase.Студенты.Join(sqlcon.DBase.УСПЕВАЕМОСТЬ, p => p.Номер_студенческого_билета, c => c.Номер_студенческого_билета,
            (p, c) => new
            {
                Номер_студбилета = p.Номер_студенческого_билета,
                Фамилия = p.Фамилия,
                Факультет = p.Факультет,
                Специальность = p.Специальность,
                Курс = p.Курс,
                Группа = p.Группа,
                Пропусков = c.Количество_пропусков_за_всё_время,
                Средняя_оценка = c.Средняя_оценка_за_всё_время
            })).ToList();

            mainlist = new List<Student>(JoinedTable.Count);
            int k = 0;
            while (k < JoinedTable.Count)
            {
                mainlist.Add(new Student(JoinedTable[k].Номер_студбилета, JoinedTable[k].Фамилия, JoinedTable[k].Факультет,
                                     JoinedTable[k].Специальность, JoinedTable[k].Курс, JoinedTable[k].Группа, JoinedTable[k].Пропусков, JoinedTable[k].Средняя_оценка));
                k++;
            }

            Total = new List<Student>(mainlist.Count);
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
        public void ResetSlider()
        {
            currentvalue = 0;

            RefreshDatabase();
        }
        private void RefreshDatabase()
        {
            buf = Total;
            LName = lname;
            StudNumber = studnumber;
            CurrentValue = currentvalue;
            SelectedType = selectedType;

            mainlist = buf;
            OnPropertyChanged("mainlist");
            buf = null;

        }
        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;

                if (buf == null)
                {
                    RefreshDatabase();
                    return;
                }

                if (selectedType == "Все")
                    buf = (from g in buf select g).ToList();
                if (selectedType == "Отличники")
                    buf = (from g in buf where g.Средняя_оценка_за_всё_время >= 8 select g).ToList();
                if (selectedType == "Неотличники")
                    buf = (from g in buf where g.Средняя_оценка_за_всё_время <= 5 select g).ToList();
                // Logic for saving to database
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
    
       
        



    }
}
