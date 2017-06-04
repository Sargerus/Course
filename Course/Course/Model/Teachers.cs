using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Model
{
    public class Teachers : IComparable
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
            Фамилия_И_О_ = fio;
            Кафедра = pulpit;
            Кабинет = cabinet;
            Предметы = subjects;
        }

        public override string ToString()
        {
            return " Номер трудовой книжки: " + Номер_трудовой_книжки + "\r\n" +
             "Фамилия И О: " + Фамилия_И_О_ + "\r\n" +
             "Кафедра: " + Кафедра + "\r\n" +
             "Кабинет: " + Кабинет + "\r\n" +
             "Предметы: " + Предметы + "\r\n";
        }

        public int CompareTo(object obj)
        {
            Teachers x = obj as Teachers;
            return this.Номер_трудовой_книжки.CompareTo(x.Номер_трудовой_книжки);
        }
    }
}
