using ASUSport.Helpers;

namespace ASUSport.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } // имя пользователя
        public string HashPassword { get; private set; } // пароль пользователя
        public string AccessCode { get; set; }

        public void SetPassword(string password)
        {
            HashPassword = PasswordHasherHelper.HashString(password);
        }
    }
}
