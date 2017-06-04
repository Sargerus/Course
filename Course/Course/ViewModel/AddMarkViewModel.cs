using Course.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    public class AddMarkViewModel:ViewModelBase
    {
        public string addstudnumb { get; set; }
        public string addsub { get; set; }
        public float addmark { get; set; }
        public DateTime adddate { get; set; }
        public GeneralCommand AddStudentCommand { get; set; }
        public AddMarkViewModel()
        {
            

            ConnectCommands();
        }
        private void ConnectCommands()
        {
            AddStudentCommand = new GeneralCommand(AddStudent, null);
        }
        private void AddStudent()
        {
           // try
           // {
                Оценки me = new Оценки();
                me.Номер_студенческого_билета = addstudnumb;
                me.Название_предмета = addsub;
                me.Оценка = addmark;
                me.Дата_выставления = adddate;

                var s = sqlcon.DBase.Set<Оценки>();
                s.Add(me);
                sqlcon.DBase.SaveChanges();
           // }
            //catch
            //{
             //   System.Media.SystemSounds.Exclamation.Play();
            //    MessageBox.Show("Error in data!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
           // }



        } 
    }
}
