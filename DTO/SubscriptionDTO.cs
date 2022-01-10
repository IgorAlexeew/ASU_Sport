namespace ASUSport.DTO
{
    /// <summary>
    /// Модель для представления информации о абонементе и создания нового абонемента
    /// </summary>
    public class SubscriptionDTO
    {
        /// <summary>
        /// Идентификатор абонемента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор спортивного объекта
        /// </summary>
        public int SportObjectId { get; set; }

        /// <summary>
        /// Тип абонемента
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Название абонемента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Кол-во посещений
        /// </summary>
        public int NumOfVisits { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public int Price { get; set; }

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
