using System.Collections.Generic;

namespace ASUSport.DTO
{
    /// <summary>
    /// Модель для отображения на заданную дату для заданного объекта событий 
    /// </summary>
    public class EventsForSportobjectDTO
    {
        public string ObjectName { get; set; }

        public int Capacity { get; set; }

        public string Date { get; set; }

        public ICollection<EventModelDTO> Events { get; set; }
    }
}
