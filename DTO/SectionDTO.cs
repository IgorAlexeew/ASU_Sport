namespace ASUSport.DTO
{
    /// <summary>
    /// Модель для создания секции
    /// </summary>
    public class SectionDTO
    {
        /// <summary>
        /// Название секции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Спортивный объект
        /// </summary>
        public int SportObject { get; set; }

        /// <summary>
        /// Продолжительность
        /// </summary>
        public int Duration { get; set; }
    }
}
