﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Model;

namespace Course.ViewModel
{

    public class TeacherWindowViewModel : ViewModelBase
    {
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
            public Teachers(string number,string fio,
                           string pulpit,string cabinet,string subjects)
            {
                Номер_трудовой_книжки = number;
                Фамилия_И_О_ = fio;
                Кафедра = pulpit;
                Кабинет = cabinet;
                Предметы = subjects;
            }
        }

        public List<Преподаватели> s { get; set; }
        public List<Teachers> teachers { get; set; }

        public TeacherWindowViewModel()
        {

            ShowTable();

        }
        private void ShowTable()
        {
            s = (sqlcon.GetTeachers()).ToList();

            teachers = new List<Teachers>(s.Count);
            int k = 0;
            while (k < s.Count)
            {
                teachers.Add(new Teachers(s[k].Номер_трудовой_книжки, s[k].Фамилия_И_О_,
                                          s[k].Кафедра, s[k].Кабинет, s[k].Предметы));
                k++;
            }
        }
    }
}