namespace ASUSport.DTO
{
    /// <summary>
    /// Форма для записи неавторизованного пользователя на событие
    /// </summary>
    public class SignUpForUnathorizedDTO
    {
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Имя нового пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail пользователя
        /// </summary>
        public string Login { get; set; }
    }
}
