using System;
using ASUSport.Models;
using System.Collections.Generic;

namespace ASUSport.DTO
{
    /// <summary>
    /// Модель для отображения данных пользователя
    /// </summary>
    public class UserModelDTO
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public int Id { get; set; }

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
        /// Роль пользователя
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Список событий
        /// </summary>
        public ICollection<EventForUserModelDTO> Events { get; set; }
    }
}
