namespace ASUSport.Models
{
    /// <summary>
    /// Ответ
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Статус ответа
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }
    }
}
