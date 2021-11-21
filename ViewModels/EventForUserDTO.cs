using System;
using ASUSport.Models;
using System.Collections.Generic;

namespace ASUSport.ViewModels
{
    /// <summary>
    /// Представление для отображения события у пользователя
    /// </summary>
    public class EventForUserDTO
    {
        /// <summary>
        /// Секции
        /// </summary>
        public Section Section { get; set; }

        /// <summary>
        /// Тренеры
        /// </summary>
        public UserDTO Trainer { get; set; }

        /// <summary>
        /// Дата и время начала событий
        /// </summary>
        public DateTime Time { get; set; }
    }
}
