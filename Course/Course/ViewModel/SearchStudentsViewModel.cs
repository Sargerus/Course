using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Course.ViewModel
{
    public class SearchStudentsViewModel : ViewModelBase
    {
        List<Student> buf;
        private string lname;
        private string studnumber;
        private int missed;
        private bool IsChanged;
        private int currentvalue;
        public GeneralCommand LessThenCommand { get; set; }
        public GeneralCommand ResetSliderCommand { get; set; }
        public int CurrentValue
        {
            get
            {
                return currentvalue;
            }
            set
            {
                currentvalue = value;
                OnPropertyChanged("CurrentValue"); 
            }
        }
        public string LName 
        {
            get { return lname; }
            set
            {
                lname = value;

                if (lname == null)
                    return;
                if (value == "")
                {
                    CheckNotify();
                    if (IsChanged)
                    {
                        buf = Total;
                        StudNumber = studnumber;
                        CurrentValue = currentvalue;
                        return;
                    }
                    
                }

                if (value != string.Empty) IsChanged = true;
                CheckNotify();
                if (IsChanged)
                {
                    if (buf == null || buf.Count==0)
                    {
                        buf = new List<Student>();
                        buf = Total;
                    }
                    buf = (from g in buf where g.Фамилия.Contains(value) select g).ToList();
                }
                else
                {
                    buf = new List<Student>();
                    buf = Total;
                }
                mainlist = buf;
                OnPropertyChanged("mainlist");
            } 
        }
        public string StudNumber {
            get { return studnumber; }
            set
            {
                studnumber = value;

                if (studnumber == null)
                    return;

                if (value == "")
                {
                    CheckNotify();
                    if (IsChanged)
                    {
                        buf = Total;
                        LName = lname;
                        CurrentValue = currentvalue;
                        return;
                    }
                    
                }

                if (value != string.Empty) IsChanged = true;
                CheckNotify();
                if (IsChanged)
                {
                    if (buf == null || buf.Count == 0)
                    {
                        buf = new List<Student>();
                        buf = Total;
                    }
                    buf = (from g in buf where g.Номер_студенческого_билета.Contains(value) select g).ToList();
                }
                else
                {
                    buf = new List<Student>();
                    buf = Total;
                }
                mainlist = buf;
                OnPropertyChanged("mainlist");
            
            }
        }
        public int Missed {
            get { return missed; }
            set
            {}
        }
        public class Student
        {
            public string Номер_студенческого_билета { get; set; }
            public string Фамилия { get; set; }
            public string Факультет { get; set; }
            public string Специальность { get; set; }
            public Nullable<short> Курс { get; set; }
            public Nullable<short> Группа { get; set; }
            public Nullable<short> Количество_пропусков_за_всё_время { get; set; }
            public Student()
            {

            }
            public Student(string stud, string lname, string faculty, string prof, Nullable<short> cour, Nullable<short> group, Nullable<short> miss)
            {
                Номер_студенческого_билета = stud;
                Фамилия = lname;
                Факультет = faculty;
                Специальность = prof;
                Курс = cour;
                Группа = group;
                Количество_пропусков_за_всё_время = miss;
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
        private List<Student> Total { get; set; }
        public List<Student> mainlist { get; set; }
        public GeneralCommand SearchCommand { get; set; }
        public SearchStudentsViewModel()
        {

            IsChanged = false;
            LessThenCommand = new GeneralCommand(LessThen);
            ResetSliderCommand = new GeneralCommand(ResetSlider);
            

            var JoinedTable = (sqlcon.DBase.Студенты.Join(sqlcon.DBase.УСПЕВАЕМОСТЬ, p => p.Номер_студенческого_билета, c => c.Номер_студенческого_билета,
                (p, c) => new
                {
                    Номер_студбилета = p.Номер_студенческого_билета,
                    Фамилия = p.Фамилия,
                    Факультет = p.Факультет,
                    Специальность = p.Специальность,
                    Курс = p.Курс,
                    Группа = p.Группа,
                    Пропусков = c.Количество_пропусков_за_всё_время
                })).ToList();

            mainlist = new List<Student>(JoinedTable.Count);
            int k = 0;
            while (k <JoinedTable.Count)
            {
                mainlist.Add(new Student(JoinedTable[k].Номер_студбилета, JoinedTable[k].Фамилия, JoinedTable[k].Факультет,
                                     JoinedTable[k].Специальность, JoinedTable[k].Курс, JoinedTable[k].Группа, JoinedTable[k].Пропусков));
                k++;
            }

            Total = new List<Student>(mainlist.Count);
            Total = mainlist;

        }
        
public void CheckNotify()
{
    if(lname==string.Empty || lname==null) 
       if (studnumber == string.Empty || studnumber==null) 
           if(currentvalue==0 || currentvalue==null) 
               IsChanged = false;

}
public void LessThen()
{

    if (currentvalue == 0)
        return;
    IsChanged = true;
    if (buf == null)
    {
        buf = new List<Student>();
        buf = Total;
    }

    if (currentvalue == 0)
    {
        CheckNotify();
        if (IsChanged)
        {
            buf = Total;
            LName = lname;
            CurrentValue = currentvalue;
            return;
        }

    }
    
    if (IsChanged)
    {
        buf = (from g in buf where g.Количество_пропусков_за_всё_время < currentvalue select g).ToList();
    }
    else
    {
        buf = (from g in Total where g.Количество_пропусков_за_всё_время < currentvalue select g).ToList();
    }

    mainlist = buf;
    OnPropertyChanged("mainlist");
}
public void ResetSlider()
{
    currentvalue = 0;
    OnPropertyChanged("CurrentValue");

    LName = lname;
    StudNumber = studnumber;
}
      
    }
}

