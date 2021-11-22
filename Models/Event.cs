using System;
using System.Collections.Generic;

namespace ASUSport.Models
{
    /// <summary>
    /// Событие
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Секция
        /// </summary>
        public virtual Section Section { get; set; }

        /// <summary>
        /// Тренер
        /// </summary>
        public virtual User Trainer { get; set; }  
        
        /// <summary>
        /// Список клиентов
        /// </summary>
        public virtual ICollection<User> Clients { get; set; }

        /// <summary>
        /// Дата и время начала события
        /// </summary>
        public DateTime Time { get; set; }
    }
}
