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
        public List<DateTime> Days { get; set; }

        /// <summary>
        /// название услуги
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Цена за данную услугу
        /// </summary>
        public int Price { get; set; }
    }
}
