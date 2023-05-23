namespace ASUSport.DTO
{
    /// <summary>
    /// Форма для ввода и отображения данных пользователя
    /// </summary>
    public class UserShortDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string LastName { get; set; }
    }
}
