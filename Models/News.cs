namespace ASUSport.Models
{
    /// <summary>
    /// Модель новости
    /// </summary>
    public class News
    {
        /// <summary>
        /// Идентификатор новости
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок новости
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст новости
        /// </summary>
        public string Text { get; set; }
    }
}
