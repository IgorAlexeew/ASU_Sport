namespace ASUSport.DTO
{
    /// <summary>
    /// Форма для ввода свойств нового события
    /// </summary>
    public class EventDTO
    {
        /// <summary>
        /// Название секции
        /// </summary>
        public int Section { get; set; }

        /// <summary>
        /// ФИО тренера
        /// </summary>
        public int Trainer { get; set; }

        /// <summary>
        /// Время начала события
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Дата начала события
        /// </summary>
        public string Date { get; set; }
    }
}
