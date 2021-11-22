using System;
using System.Collections.Generic;

namespace ASUSport.ViewModels
{
    /// <summary>
    /// блок информации о объекте для главной страницы
    /// </summary>
    public class SportObjectInfo
    {
        /// <summary>
        /// Название объекта
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Наименее загруенные дни
        /// </summary>
        public List<DateTime> Days { get; set; }
    }
}
