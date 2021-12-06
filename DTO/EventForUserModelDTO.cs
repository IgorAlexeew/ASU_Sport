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
        public TrainerDTO Trainer { get; set; }

        /// <summary>
        /// Время начала событий
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Дата начала событий
        /// </summary>
        public string Date { get; set; }
    }
}
