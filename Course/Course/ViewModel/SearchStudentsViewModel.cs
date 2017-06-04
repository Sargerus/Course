using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Microsoft.Win32;
using Course.Views;

namespace Course.ViewModel
{
    public class SearchStudentsViewModel : ViewModelBase
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
                return " Номер студенческого билета: " + Номер_студенческого_билета.ToString() + "\r\n" +
                       "Фамилия: " + Фамилия.ToString() + "\r\n" +
                       "Факультет: " + Факультет.ToString() + "\r\n" +
                       "Специальность: " + Специальность.ToString() + "\r\n" +
                       "Курс: " + Курс.ToString() + "\r\n" +
                       "Группа: " + Группа.ToString() + "\r\n" +
                       "Подгруппа: " + Количество_пропусков_за_всё_время.ToString() + "\r\n";
            }


        }
        List<Student> buf;
        private List<Student> Total { get; set; }
        public List<Student> mainlist { get; set; }
        public ICollection<string> StudentsType { get; set; }
      
        private string lname;
        private string studnumber;
        private int currentvalue;
        private string selectedType;

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
        public string StudNumber
        {
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
       

     
        public GeneralCommand BackCommand { get; set; }
        public GeneralCommand SaveCommand { get; set; }
        public GeneralCommand SearchTeachersCommand { get; set; }


   
        
       
        public SearchStudentsViewModel()
        {
            ConnectCommands();
            CreateTable();
         
            Total = new List<Student>(mainlist.Count);
            Total = mainlist;

            StudentsType = new Collection<string>
                                    { 
                                        "All",
                                        "Excellent", 
                                        "Bad", 
                                    };
            SelectedType = StudentsType.First();

        }

        private void ConnectCommands()
        {
            BackCommand = new GeneralCommand(Back, null);
            SaveCommand = new GeneralCommand(Save, null);
            SearchTeachersCommand = new GeneralCommand(SearchTeacher, null);
            ChangeLangEngCommand = new GeneralCommand(ChangeLangEng, null);
            ChangeLangRusCommand = new GeneralCommand(ChangeLangRus, null);
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
                   Средняя_оценка = c.Средняя_оценка_за_поледнюю_сессию
               })).ToList();

            mainlist = new List<Student>(JoinedTable.Count);
            int k = 0;

            if (AccesLevel == AccesLevels.User)
            {

                var buf = JoinedTable.Select(g => g).Where(g => g.Номер_студбилета == ViewModelBase.StudNumber).ToList();
                if (buf.Count > 0)
                    mainlist.Add(new Student(buf[0].Номер_студбилета, buf[0].Фамилия, buf[0].Факультет,
                                     buf[0].Специальность, buf[0].Курс, buf[0].Группа, buf[0].Пропусков, buf[0].Средняя_оценка));

            }
            else
                while (k < JoinedTable.Count)
                {
                    mainlist.Add(new Student(JoinedTable[k].Номер_студбилета, JoinedTable[k].Фамилия, JoinedTable[k].Факультет,
                                         JoinedTable[k].Специальность, JoinedTable[k].Курс, JoinedTable[k].Группа, JoinedTable[k].Пропусков, JoinedTable[k].Средняя_оценка));
                    k++;
                }
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

                if(selectedType=="All")
                    buf = (from g in buf select g).ToList();
                if (selectedType == "Excellent")
                    buf = (from g in buf where g.Средняя_оценка_за_всё_время>=8 select g).ToList();
                if (selectedType == "Bad")
                    buf = (from g in buf where g.Средняя_оценка_за_всё_время <= 5 select g).ToList();
                // Logic for saving to database
            }
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
       
        

        
        


    }
}

