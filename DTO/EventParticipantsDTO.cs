using System.Collections.Generic;

namespace ASUSport.DTO
{
    /// <summary>
    /// Модель для вывода участников события для даты и спортивного объекта
    /// </summary>
    public class EventParticipantsDTO
    {
        /// <summary>
        /// Время начала и окончания события
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// Тренер
        /// </summary>
        public UserDataDTO Trainer { get; set; }

        /// <summary>
        /// Название секции
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// Клиенты
        /// </summary>
        public List<UserDataDTO> Clients { get; set; }
    }
}
