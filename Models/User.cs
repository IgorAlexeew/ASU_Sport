using ASUSport.Helpers;
using System.Collections.Generic;

namespace ASUSport.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        private string hashPassword;
        public string Password
        {
            get { return hashPassword; }
            set
            {
                hashPassword = PasswordHasherHelper.HashString(value);
            }
        } // пароль пользователя
        public string AccessCode { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
