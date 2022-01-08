namespace ASUSport.DTO
{
    /// <summary>
    /// Форма для изменения данных события
    /// </summary>
    public class UpdateEventDTO
    {
        /// <summary>
        /// Идентификатор обновляемого события
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Идентификатор секции
        /// </summary>
        public int? SectionId { get; set; }

        /// <summary>
        /// Идентификатор тренера
        /// </summary>
        public int? TrainerId { get; set; }

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
