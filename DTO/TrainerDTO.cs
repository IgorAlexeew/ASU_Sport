namespace ASUSport.DTO
{
    /// <summary>
    /// Модель с информацией о тренере
    /// </summary>
    public class TrainerDTO
    {
        /// <summary>
        /// Идентификатор тренера
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия тренера
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Имя тренера
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Отчество тренера
        /// </summary>
        public string LastName { get; set; }
    }
}
