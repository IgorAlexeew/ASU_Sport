using System;
using ASUSport.Models;
using ASUSport.DTO;

namespace ASUSport.DTO
{
    /// <summary>
    /// Модель для отображения события у пользователя в профиле
    /// </summary>
    public class EventForUserModelDTO
    {
        /// <summary>
        /// Секции
        /// </summary>
        public SectionForUserDTO Section { get; set; }

        /// <summary>
        /// Тренеры
        /// </summary>
        public TrainerModelDTO Trainer { get; set; }

        /// <summary>
        /// Дата и время начала событий
        /// </summary>
        public DateTime Time { get; set; }
    }
}
