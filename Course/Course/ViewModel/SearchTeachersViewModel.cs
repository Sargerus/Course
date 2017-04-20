using Course.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ViewModel
{
    public class SearchTeachersViewModel : ViewModelBase
    {
        private string booknumber;
        private string lname;

        public class Teachers
        {
            public string Номер_трудовой_книжки { get; set; }
            public string Фамилия_И_О_ { get; set; }
            public string Кафедра { get; set; }
            public string Кабинет { get; set; }
            public string Предметы { get; set; }

            public Teachers(string number, string fio,
                          string pulpit, string cabinet, string subjects)
            {
                Номер_трудовой_книжки = number;
                Фамилия_И_О_ = fio;
                Кафедра = pulpit;
                Кабинет = cabinet;
                Предметы = subjects;
            }
        }
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
                    buf = (from g in buf where g.Номер_трудовой_книжки.Contains(value) select g).ToList();
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
                    buf = (from g in buf where g.Фамилия_И_О_.Contains(value) select g).ToList();
                else return;
            }
        }
        List<Teachers> buf;
        private List<Teachers> Total { get; set; }
        public List<Teachers> mainlist { get; set; }

        public SearchTeachersViewModel()
        {
            List<Преподаватели> buffer = (sqlcon.GetTeachers()).ToList();

            mainlist = new List<Teachers>(buffer.Count);
            int k = 0;
            while (k < buffer.Count)
            {
                mainlist.Add(new Teachers(buffer[k].Номер_трудовой_книжки, buffer[k].Фамилия_И_О_,
                                          buffer[k].Кафедра, buffer[k].Кабинет, buffer[k].Предметы));
                k++;
            }
            Total = mainlist;
            buf = null;
        }

        private void RefreshDatabase()
        {
            buf = Total;
            LName = lname;
            BookNumber = booknumber;

            mainlist = buf;
            OnPropertyChanged("mainlist");
            buf = null;

        }
    }
}
