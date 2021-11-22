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

        private string hashPassword;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password
        {
            get { return hashPassword; }
            set
            {
                hashPassword = PasswordHasherHelper.HashString(value);
            }
        }
        /// <summary>
        /// Код доступа
        /// </summary>
        public string AccessCode { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Список событий
        /// </summary>
        public virtual ICollection<Event> Events { get; set; }
    }
}
