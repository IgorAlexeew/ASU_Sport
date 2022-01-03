using System.Collections.Generic;

namespace ASUSport.DTO
{
    /// <summary>
    /// Модель для вывода списка событий с клиентами для даты и спортивного объекта
    /// </summary>
    public class EventsWithClientsDTO
    {
        /// <summary>
        /// Название спортивного объекта
        /// </summary>
        public string SportObject { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Время события и клиенты
        /// </summary>
        public List<EventParticipantsDTO> EventParticipants { get; set; }
    }
}
