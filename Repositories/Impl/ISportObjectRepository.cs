using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Models;
using ASUSport.ViewModels;

namespace ASUSport.Repositories.Impl
{
    public interface ISportObjectRepository
    {
        /// <summary>
        /// Получить информацию о объектах для главной страницы
        /// </summary>
        /// <returns>Информация о спорт объекте</returns>
        public List<SportObjectInfo> GetInfo();

        /// <summary>
        /// Получить дату ближайшего дня на этой неделе
        /// </summary>
        /// <param name="today">Текущая дата</param>
        /// <param name="day">День недели</param>
        /// <returns>Дата</returns>
        public DateTime GetNearestDay(DateTime today, DayOfWeek day);
    }
}
