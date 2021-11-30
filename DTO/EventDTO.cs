namespace ASUSport.DTO
{
    /// <summary>
    /// Форма для ввода свойств события
    /// </summary>
    public class EventDTO
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
    }
}
