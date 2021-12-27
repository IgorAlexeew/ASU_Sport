namespace ASUSport.DTO
{
    /// <summary>
    /// Модель для изменения секции
    /// </summary>
    public class SectionForUpdateDTO
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
        /// Спортивный объект
        /// </summary>
        public int? SportObject { get; set; }

        /// <summary>
        /// Продолжительность
        /// </summary>
        public int? Duration { get; set; }
    }
}
