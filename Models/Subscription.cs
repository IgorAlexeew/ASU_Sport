using System.Collections.Generic;

namespace ASUSport.Models
{
    /// <summary>
    /// Абонемент
    /// </summary>
    public class Subscription
    {
        /// <summary>
        /// Идентификатор абонемента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Спортивный объект
        /// </summary>
        public virtual SportObject SportObject { get; set; }
        public int SportObjectId { get; set; }

        /// <summary>
        /// Тип абонемента
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Название абонемента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Кол-во посещений
        /// </summary>
        public int NumOfVisits { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Время открытия
        /// </summary>
        public string StartingTime { get; set; }

        /// <summary>
        /// Время закрытия
        /// </summary>
        public string ClosingTime { get; set; }

        /// <summary>
        /// Список обладателей этого абонемента
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
