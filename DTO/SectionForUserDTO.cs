namespace ASUSport.DTO
{
    /// <summary>
    /// Представление для секции  в профиле пользователя
    /// </summary>
    public class SectionForUserDTO
    {
        /// <summary>
        /// Название секции
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// Продолжительность занятия
        /// </summary>
        public int Duration { get; set; }
    }
}
