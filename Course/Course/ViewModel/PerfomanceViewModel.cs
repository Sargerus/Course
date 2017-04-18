using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Model;
using System.Windows;

namespace Course.ViewModel
{
    public class PerfomanceViewModel : ViewModelBase
    {
        public GeneralCommand ShowStudentCommand { get; set; }
        public GeneralCommand ShowTeachersCommand { get; set; }
        public List<BufferedPerfomance> Perfomance { get; set; }
        private List<УСПЕВАЕМОСТЬ> Buffer { get; set; }
        public class BufferedPerfomance
        {
            public BufferedPerfomance(string numb, Nullable<double> sozvv, Nullable<double> sozps, Nullable<short> kpzvv,
                Nullable<short> kprzvv, Nullable<double> sozpa)
            {
                Номер_студбилета = numb;
                Средняя_оценка_всего = sozvv;
                Средняя_оценка_сессия = sozps;
                Пересдачи_всего = kpzvv;
                Пропуски_всего = kprzvv;
                Средняя_аттест = sozpa;
                    
            }
            public string Номер_студбилета { get; set; }
            public Nullable<double> Средняя_оценка_всего{ get; set; }
            public Nullable<double> Средняя_оценка_сессия { get; set; }
            public Nullable<short> Пересдачи_всего { get; set; }
            public Nullable<short> Пропуски_всего { get; set; }
            public Nullable<double> Средняя_аттест { get; set; }
        }
        public PerfomanceViewModel()
        {
            ShowTeachersCommand = new GeneralCommand(ShowTeachers, null);
            ShowStudentCommand = new GeneralCommand(ShowStudents, null);
            FromBufferToList();
        }
        public void ShowTeachers()
        {
            var NewWindow = new TeachersWindow();
            NewWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = NewWindow;
        }
        public void ShowStudents()
        {
            var NewWindow = new StudentMain();
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
                Perfomance.Add(new BufferedPerfomance(Buffer[i].Номер_студенческого_билета, Buffer[i].Средняя_оценка_за_всё_время,
                    Buffer[i].Средняя_оценка_за_поледнюю_сессию, Buffer[i].Количество_пересдач_за_всё_время,
                    Buffer[i].Количество_пропусков_за_всё_время, Buffer[i].Средняя_оценка_за_промежуточную_аттестацию));
            }
        }
    }
}
