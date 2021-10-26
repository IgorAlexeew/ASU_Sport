using ASUSport.Helpers;

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
    }
}
