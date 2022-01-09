namespace ASUSport.DTO
{
    /// <summary>
    /// Форма для вывода информации о секции
    /// </summary>
    public class SectionInfoDTO
    {
        /// <summary>
        /// Идентификатор секции
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название секции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор спортивного объекта
        /// </summary>
        public int SportObjectId { get; set; }

        /// <summary>
        /// Продолжительность секции
        /// </summary>
        public int Duration { get; set; }
    }
}
