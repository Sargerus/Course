using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Course.Model;
using Course.Views;
using Microsoft.Win32;
using System.Windows.Controls;

namespace Course.ViewModel
{
    public class StudentWindowViewModel : ViewModelBase
    {
      
        public class Student :IComparable
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
                return ") Номер студенческого билета: " + Номер_студенческого_билета.ToString() + " \r\n" +
                       "  Фамилия: " + Фамилия.ToString() + "\r\n" +
                       "  Факультет: " + Факультет.ToString() + "\r\n" +
                       "  Специальность: " + Специальность.ToString() + "\r\n" +
                       "  Курс: " + Курс.ToString() + "\r\n" +
                       "  Группа: " + Группа.ToString() + "\r\n" +
                       "  Пропусков за всё время: " + Количество_пропусков_за_всё_время.ToString() + "\r\n" + 
                       "  Средняя оценка за всё время: " + Средняя_оценка_за_всё_время.ToString() + "\r\n";
            }


            public int CompareTo(object obj)
            {
                Student other = obj as Student;
                if (this.Средняя_оценка_за_всё_время > other.Средняя_оценка_за_всё_время)
                    return 1;
                if (this.Средняя_оценка_за_всё_время < other.Средняя_оценка_за_всё_время)
                    return -1;
                return 0;

            }
        }
        
        public List<Студенты> s { get; set; }
        public List<Student> student { get; set; }
        public GeneralCommand ShowTeachersCommand { get; set; }
        public GeneralCommand ShowPerfomanceCommand { get; set; }
        public GeneralCommand SaveCommand { get; set; }
        public GeneralCommand SearchStudentsCommand { get; set; }
        public GeneralCommand SearchTeachersCommand { get; set; }
        public GeneralCommand EditTeachersCommand { get; set; }
        public GeneralCommand EditStudentsCommand { get; set; }
        public GeneralCommand AttestationCommand { get; set; }
        public GeneralCommand ChangeLangRusCommand { get; set; }
        public GeneralCommand ChangeLangEngCommand { get; set; }

        public StudentWindowViewModel()
        {
            
            ShowTeachersCommand = new GeneralCommand(ShowTeachers,null);
            ShowPerfomanceCommand = new GeneralCommand(ShowPerfomance, null);
            SaveCommand = new GeneralCommand(Save, null);
            SearchStudentsCommand = new GeneralCommand(SearchStudents, null);
            SearchTeachersCommand = new GeneralCommand(SearchTeacher, null);
            EditStudentsCommand = new GeneralCommand(EditStudents, null);
            EditTeachersCommand = new GeneralCommand(EditTeachers, null);
            AttestationCommand = new GeneralCommand(Attestation, null);
            ChangeLangRusCommand = new GeneralCommand(ChangeLangRus, null);
            ChangeLangEngCommand = new GeneralCommand(ChangeLangEng, null);
            
            ShowTable();
            
        }
        public void ChangeLangRus()
        {
            //string str = param as string;
            //switch (str)
            //{
            //    case "Rus": {MessageBox.Show("Rus"); break;}
            //    case "Eng": { MessageBox.Show("Eng"); break; }
            //
                Language = new System.Globalization.CultureInfo("ru-RU");
        }
        public void ChangeLangEng()
        {
                Language = new System.Globalization.CultureInfo("en-US");
        
        }
        public void EditTeachers()
        {
            if (AccesLevel == AccesLevels.Dean)
            {
                var NewWindow = new EditTeachersWindow();

                NewWindow.Top = Application.Current.MainWindow.Top;
                NewWindow.Left = Application.Current.MainWindow.Left;
                NewWindow.Height = Application.Current.MainWindow.ActualHeight;
                NewWindow.Width = Application.Current.MainWindow.ActualWidth;


                NewWindow.Show();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = NewWindow;
            }
            else MessageBox.Show("Not enougth rights!!!");
        }
        public void Attestation()
        {
            var NewWindow = new AttestationWindow(); 
            NewWindow.Top = Application.Current.MainWindow.Top;
            NewWindow.Left = Application.Current.MainWindow.Left;
            NewWindow.Height = Application.Current.MainWindow.ActualHeight;
            NewWindow.Width = Application.Current.MainWindow.ActualWidth;

            NewWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = NewWindow;
        }
        public void EditStudents()
        {
            if (AccesLevel != AccesLevels.User)
            {
                var NewWindow = new EditStudentsWindow();
                NewWindow.Top = Application.Current.MainWindow.Top;
                NewWindow.Left = Application.Current.MainWindow.Left;
                NewWindow.Height = Application.Current.MainWindow.ActualHeight;
                NewWindow.Width = Application.Current.MainWindow.ActualWidth;
                NewWindow.Show();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = NewWindow;
            }
            
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
                        System.IO.File.AppendAllText(savefiledialog.FileName, (g + 1).ToString());
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
        public void ShowTeachers()
        {
            var NewWindow = new TeachersWindow();
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
            //logic work with data for DataGrid
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
            

            
            student = new List<Student>(JoinedTable.Count);
           
                
            int k = 0;
            if (StudNumber != null || StudNumber != String.Empty)
            {
                while (k < JoinedTable.Count)
                {

                    if (JoinedTable[k].Номер_студбилета == StudNumber)
                        student.Add(new Student(JoinedTable[k].Номер_студбилета, JoinedTable[k].Фамилия, JoinedTable[k].Факультет,
                                             JoinedTable[k].Специальность, JoinedTable[k].Курс, JoinedTable[k].Группа, JoinedTable[k].Пропусков, JoinedTable[k].Средняя_оценка));

                    k++;
                }
            }
            else 
            {
                while (k < JoinedTable.Count)
                {
                        student.Add(new Student(JoinedTable[k].Номер_студбилета, JoinedTable[k].Фамилия, JoinedTable[k].Факультет,
                                             JoinedTable[k].Специальность, JoinedTable[k].Курс, JoinedTable[k].Группа, JoinedTable[k].Пропусков, JoinedTable[k].Средняя_оценка));
                        k++;
                }
            }   
            
            student.Sort();
            
        }
    }
}
