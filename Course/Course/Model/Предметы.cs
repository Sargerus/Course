//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Course.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Предметы
    {
        public Предметы()
        {
            this.Преподаватели = new HashSet<Преподаватели>();
        }
    
        public string Название_предмета { get; set; }
        public string Код_предмета { get; set; }
    
        public virtual ICollection<Преподаватели> Преподаватели { get; set; }
    }
}
