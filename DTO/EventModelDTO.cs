namespace ASUSport.DTO
{
    /// <summary>
    /// Модель для отображения свойств события
    /// </summary>
    public class EventModelDTO
    {
        /// <summary>
        /// Название секции
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// ФИО тренера
        /// </summary>
        public string TrainerName { get; set; }

        /// <summary>
        /// Дата и время начала события
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Продолжительность события
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Свободные места
        /// </summary>
        public int FreeSpaces { get; set; }
    }
}
