﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Model;
using System.Windows;
using Course.Views;

namespace Course.ViewModel
{
    public class PerfomanceViewModel : ViewModelBase
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
        public GeneralCommand ShowStudentCommand { get; set; }
        public GeneralCommand ShowTeachersCommand { get; set; }
        public GeneralCommand SearchStudentsCommand { get; set; }
        public GeneralCommand SearchTeachersCommand { get; set; }
       
       

  
        public List<BufferedPerfomance> Perfomance { get; set; }
        private List<УСПЕВАЕМОСТЬ> Buffer { get; set; }
        public class BufferedPerfomance
        {
            public BufferedPerfomance(string numb, string fio, Nullable<double> sozps, Nullable<short> kpzvv,
                Nullable<short> kprzvv, Nullable<double> sozpa)
            {
                Номер_студбилета = numb;
                Фамилия = fio;
                Средняя_оценка_сессия = sozps;
                Пересдачи_всего = kpzvv;
                Пропуски_всего = kprzvv;
                Средняя_аттест = sozpa;
                    
            }
            public string Номер_студбилета { get; set; }
            public string Фамилия { get; set; }
            public Nullable<double> Средняя_оценка_сессия { get; set; }
            public Nullable<short> Пересдачи_всего { get; set; }
            public Nullable<short> Пропуски_всего { get; set; }
            public Nullable<double> Средняя_аттест { get; set; }
        }


        public PerfomanceViewModel()
        {

            ConnectCommands();
            FromBufferToList();
        }

      


        private void ConnectCommands()
        {
            ShowTeachersCommand = new GeneralCommand(ShowTeachers, null);
            ShowStudentCommand = new GeneralCommand(ShowStudents, null);
            SearchStudentsCommand = new GeneralCommand(SearchStudents, null);
            SearchTeachersCommand = new GeneralCommand(SearchTeacher, null);
            ChangeLangEngCommand = new GeneralCommand(ChangeLangEng, null);
            ChangeLangRusCommand = new GeneralCommand(ChangeLangRus, null);
           
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
        private void FromBufferToList()
        {
            Buffer = (sqlcon.GetPerfomance()).ToList();
            Perfomance = new List<BufferedPerfomance>(Buffer.Count);

            for (int i = 0; i < Buffer.Count; i++)
            {
                if (StudNumber != null && StudNumber != String.Empty)
                {
                    if (Buffer[i].Номер_студенческого_билета.Equals(StudNumber))
                        Perfomance.Add(new BufferedPerfomance(Buffer[i].Номер_студенческого_билета, Buffer[i].Фамилия,
                            Buffer[i].Средняя_оценка_за_поледнюю_сессию, Buffer[i].Количество_пересдач_за_всё_время,
                            Buffer[i].Количество_пропусков_за_всё_время, Buffer[i].Средняя_оценка_за_промежуточную_аттестацию));
                }
                else
                {
                    Perfomance.Add(new BufferedPerfomance(Buffer[i].Номер_студенческого_билета, Buffer[i].Фамилия,
                            Buffer[i].Средняя_оценка_за_поледнюю_сессию, Buffer[i].Количество_пересдач_за_всё_время,
                            Buffer[i].Количество_пропусков_за_всё_время, Buffer[i].Средняя_оценка_за_промежуточную_аттестацию));
                }
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
       
    }
}
