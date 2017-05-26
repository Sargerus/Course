using Course.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ViewModel
{
   public class AddTeacherWindowViewModel:ViewModelBase
   {
       public string addworknumb { get; set; }
       public string addfio { get; set; }
       public string addkaf { get; set; }
       public string addkab { get; set; }

       public GeneralCommand AddTeacherCommand { get; set; }
       public AddTeacherWindowViewModel()
       {
           AddTeacherCommand = new GeneralCommand(AddTeacher, null);
       }
       public void AddTeacher()
       {
           Преподаватели me = new Преподаватели();
           me.Кабинет = addkab;
           me.Кафедра = addkaf;
           me.Номер_трудовой_книжки = addworknumb;
           me.Фамилия_И_О_ = addfio;

           var s = sqlcon.DBase.Set<Преподаватели>();
           s.Add(me);

           sqlcon.DBase.SaveChanges();
       }
    }
}
