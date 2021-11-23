using System;
using ASUSport.Models;
using System.Collections.Generic;

namespace ASUSport.ViewModels
{
    /// <summary>
    /// Данные пользователя
    /// </summary>
    public class UserViewModel
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

        /// <summary>
        /// Список событий
        /// </summary>
        public ICollection<EventForUserDTO> Events { get; set; }
    }
}
