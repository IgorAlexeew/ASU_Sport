namespace ASUSport.DTO
{
    /// <summary>
    /// Форма для создания спортивного объекта
    /// </summary>
    public class SportObjectDTO
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Вместимость
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Местоположение
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Время открытия
        /// </summary>
        public string StartingTime { get; set; }

        /// <summary>
        /// Время закрытия
        /// </summary>
        public string ClosingTime { get; set; }
    }
}
