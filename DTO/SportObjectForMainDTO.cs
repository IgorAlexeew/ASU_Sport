using System;
using System.Collections.Generic;

namespace ASUSport.DTO
{
    /// <summary>
    /// Модель с информацией о объекте для главной страницы
    /// </summary>
    public class SportObjectForMainDTO
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название объекта
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Рабочее время
        /// </summary>
        public string WorkingHours { get; set; }

        /// <summary>
        /// Наименее загруженные дни
        /// </summary>
        public List<string> Days { get; set; }

        /// <summary>
        /// Название услуги
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Цена за данную услугу
        /// </summary>
        public int? Price { get; set; }
    }
}
