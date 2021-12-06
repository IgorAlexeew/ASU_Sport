using System;

namespace ASUSport.DTO
{
    /// <summary>
    /// Форма для ввода данных пользователя
    /// </summary>
    public class UserDataDTO
    {
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
