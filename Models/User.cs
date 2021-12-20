using ASUSport.Helpers;
using System.Collections.Generic;

namespace ASUSport.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string HashPassword { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }

        /// <summary>
        /// Список событий
        /// </summary>
        public virtual ICollection<Event> Events { get; set; }

        /// <summary>
        /// Список абонементов
        /// </summary>
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
