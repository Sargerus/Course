using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Model
{
    public class SqlConnection : BaseConneciton
    {
        public Entities DBase;
        public SqlConnection()
        {
            DBase = new Entities();
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
    }
}
