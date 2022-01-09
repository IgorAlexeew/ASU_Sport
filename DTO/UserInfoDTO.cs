namespace ASUSport.DTO
{
    /// <summary>
    /// Модель со всей информацией пользователя
    /// </summary>
    public class UserInfoDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string HashPassword { get; set; }

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public string DateOfBirth { get; set; }
    }
}
