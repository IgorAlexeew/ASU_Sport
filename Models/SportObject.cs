namespace ASUSport.Models
{
    /// <summary>
    /// Спортивный объект
    /// </summary>
    public class SportObject
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public int Id { get; set; }

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
    }
}
