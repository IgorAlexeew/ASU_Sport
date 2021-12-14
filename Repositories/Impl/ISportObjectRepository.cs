using System;
using System.Collections.Generic;
using ASUSport.Models;
using ASUSport.DTO;

namespace ASUSport.Repositories.Impl
{
    public interface ISportObjectRepository
    {
        /// <summary>
        /// Получить информацию о объектах для главной страницы
        /// </summary>
        /// <returns>Информация о спорт объекте</returns>
        public List<SportObjectForMainDTO> GetInfo();
        
        /// <summary>
        /// Получить дату ближайшего дня на этой неделе
        /// </summary>
        /// <param name="today">Текущая дата</param>
        /// <param name="day">День недели</param>
        /// <returns>Дата</returns>
        public DateTime GetNearestDay(DateTime today, DayOfWeek day);

        /// <summary>
        /// Поулчить список с идентификаторами и названиями объектов
        /// </summary>
        /// <returns>Список объектов</returns>
        public List<object> GetSportObjectIds();
    }
}
