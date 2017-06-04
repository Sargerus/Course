using Course.Model;
using Course.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    public class SearchTeachersViewModel : ViewModelBase
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
      
        private string booknumber;
        private string lname;

        
        public string BookNumber
        { 
            get { return booknumber; }
            set
            {
                booknumber = value;

                if (buf == null)
                {
                    RefreshDatabase();
                    return;
                }

                if (booknumber != string.Empty && booknumber != null)
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


        List<Teachers> buf;
        private List<Teachers> Total { get; set; }
        public List<Teachers> mainlist { get; set; }
        public List<Teachers> Full { get; set; }


        public GeneralCommand BackCommand { get; set; }
        public GeneralCommand SaveCommand { get; set; }
        public GeneralCommand SearchStudentsCommand { get; set; }

        public SearchTeachersViewModel()
        {
            Full = new List<Teachers>();
            ConnectCommands();
            CreateTable();
            Total = mainlist;
            buf = null;
        }

        private void ConnectCommands()
        {
            SaveCommand = new GeneralCommand(Save, null);
            SearchStudentsCommand = new GeneralCommand(SearchStudents, null);
            BackCommand = new GeneralCommand(Back, null);
            ChangeLangEngCommand = new GeneralCommand(ChangeLangEng, null);
            ChangeLangRusCommand = new GeneralCommand(ChangeLangRus, null);
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
            Full = new List<Teachers>(table.Count());
            for (int k = 0; k < table.Count; k++)
            {
                mainlist.Add(new Teachers(table[k].Numb, table[k].Fam,
                                       table[k].Kaf, table[k].Kab, table[k].Naz));
                Full.Add(new Teachers(table[k].Numb, table[k].Fam,
                                       table[k].Kaf, table[k].Kab, table[k].Naz));
            }

            Sort();
        }
        private void Sort()
        {
            mainlist.Sort();
            Full.Sort();
            SortThis(mainlist);
        }
        private void SortThis(List<Teachers> obj)
        {
            if (obj.Count == 0 || obj == null)
                return;
            var z = obj[0];
            for (int i = 1; i < obj.Count; i++)
            {
                if (z.Номер_трудовой_книжки.Equals(obj[i].Номер_трудовой_книжки))
                {
                    obj[i].Номер_трудовой_книжки = null;
                    obj[i].Фамилия_И_О_ = null;
                    obj[i].Кафедра = null;
                    obj[i].Кабинет = null;
                }
                else z = obj[i];
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
        private void RefreshDatabase()
        {
            CreateTable();
            buf = Full;
            LName = lname;
            BookNumber = booknumber;

            mainlist = buf;
            SortThis(mainlist);
            OnPropertyChanged("mainlist");
            buf = null;

        }
    }
}
