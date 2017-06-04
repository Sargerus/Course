﻿using Course.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    public class EditTeachersViewModel : ViewModelBase
    {

        private string lname;
        private string trudnumber;

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
            public Teachers(string number, string fio,
                           string pulpit, string cabinet, string subjects)
            {
                Номер_трудовой_книжки = number;
                Фамилия_И_О_          = fio;
                Кафедра               = pulpit;
                Кабинет               = cabinet;
                Предметы              = subjects;
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
          
        List<Teachers> buf;

        private List<Teachers> Total { get; set; }
        public List<Teachers> mainlist { get; set; }
        
        
        public GeneralCommand BackCommand { get; set; }
        public GeneralCommand SaveCommand { get; set; }
        public GeneralCommand ClearCommand { get; set; }
        public GeneralCommand BeginSeaCommand { get; set; }
        public GeneralCommand SearchStudentsCommand { get; set; }
        public GeneralCommand SearchTeachersCommand { get; set; }
        public GeneralCommand AddNewCommand { get; set; }       
        
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
                {

                    List<Teachers> k = new List<Teachers>();
                    foreach (var g in buf)
                        if (g.Номер_трудовой_книжки != null)
                            k.Add(g);

                    buf = k;

                    buf = (from g in buf where g.Фамилия_И_О_.Contains(value) select g).ToList();
                }
                else return;
            }
        }
        public string TrudNumber 
        {
            get { return trudnumber; }
            set
            {
                trudnumber = value;

                if (buf == null)
                {
                    RefreshDatabase();
                    return;
                }

                if (trudnumber != string.Empty && trudnumber != null)
                {
                    List<Teachers> k = new List<Teachers>();
                    foreach (var g in buf)
                        if (g.Номер_трудовой_книжки != null)
                            k.Add(g);

                    buf = k;
                    buf = (from g in buf where g.Номер_трудовой_книжки.Contains(value) select g).ToList();
                }
                else return;
            }
        }


        public EditTeachersViewModel()
        {
            ConnectCommands();
            CreateTable();

            Total = new List<Teachers>(mainlist.Count);
            Total = mainlist;
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
        public void Clear()
        {
            LName = String.Empty;
            OnPropertyChanged("LName");

            TrudNumber = String.Empty;
            OnPropertyChanged("TrudNumber");

            mainlist = Total;
            OnPropertyChanged("mainlist");
        }

        private void CreateTable()
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

            mainlist = new List<Teachers>(table.Count());

            int k = 0;
            mainlist.Add(new Teachers(table[k].Numb, table[k].Fam,
                                       table[k].Kaf, table[k].Kab, table[k].Naz));
            k++;

            var z = table[0];
            while (k < table.Count())
            {
                if (table[k].Fam.Equals(z.Fam))
                    mainlist.Add(new Teachers(null, null,
                                             null, null, table[k].Naz));
                else
                {
                    mainlist.Add(new Teachers(table[k].Numb, table[k].Fam,
                                       table[k].Kaf, table[k].Kab, table[k].Naz));
                    z = table[k];
                }
                k++;
            }           
        }
        private void ConnectCommands()
        {
            SaveCommand = new GeneralCommand(Save, null);
            BackCommand = new GeneralCommand(Back, null);
            ClearCommand = new GeneralCommand(Clear, null);
            SearchStudentsCommand = new GeneralCommand(SearchStudents, null);
            SearchTeachersCommand = new GeneralCommand(SearchTeacher, null);
            AddNewCommand = new GeneralCommand(AddNew, null);
            ChangeLangEngCommand = new GeneralCommand(ChangeLangEng, null);
            ChangeLangRusCommand = new GeneralCommand(ChangeLangRus, null);
        }

        public void AddNew()
        {
            var NewWindow = new AddTeacherWindow();
            NewWindow.Top = Application.Current.MainWindow.Top;
            NewWindow.Left = Application.Current.MainWindow.Left;
            NewWindow.Show();
        }
        public void RefreshDatabase()
        {
            buf = Total;
            LName = lname;
            TrudNumber = trudnumber;

            mainlist = buf;
            OnPropertyChanged("mainlist");
            buf = null;
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
