﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Model
{
    public class SqlConnection : BaseConneciton
    {
        public UniverModel DBase;
        public SqlConnection()
        {
            DBase = new UniverModel();
        }
        public IQueryable<Студенты> GetStudents()
        {
            IQueryable<Студенты> retVal = from g in this.DBase.Студенты select g;
            return retVal;
        }
        public IQueryable<Преподаватели> GetTeachers()
        {
            IQueryable<Преподаватели> retVal = from g in this.DBase.Преподаватели select g;
            return retVal;
        }
        public IQueryable<УСПЕВАЕМОСТЬ> GetPerfomance()
        {
            IQueryable<УСПЕВАЕМОСТЬ> retVal = from g in this.DBase.УСПЕВАЕМОСТЬ select g;
            return retVal;
        }
    }
}
