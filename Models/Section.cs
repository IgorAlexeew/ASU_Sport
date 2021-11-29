namespace ASUSport.Models
{
    /// <summary>
    /// Секция
    /// </summary>
    public class Section
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
        public virtual SportObject SportObject { get; set; }
        public int SportObjectId { get; set; }

        /// <summary>
        /// Продолжительность секции
        /// </summary>
        public int Duration { get; set; }
    }
}
