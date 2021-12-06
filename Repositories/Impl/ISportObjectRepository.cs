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
        /// <param name="num">Кол-во объектов</param>
        /// <returns>Информация о спорт объекте</returns>
        public List<SportObjectForMainDTO> GetInfo(int num);
        
        /// <summary>
        /// Получить дату ближайшего дня на этой неделе
        /// </summary>
        /// <param name="today">Текущая дата</param>
        /// <param name="day">День недели</param>
        /// <returns>Дата</returns>
        public DateTime GetNearestDay(DateTime today, DayOfWeek day);

        /// <summary>
        /// Получить для конкретной даты и названия объекта список событий
        /// </summary>
        /// <param name="name">Название объекта</param>
        /// <param name="date">Дата</param>
        /// <returns>Список событий</returns>
        public EventsForSportobjectDTO GetEventByDateSportObject(string name, string date);

        /// <summary>
        /// Поулчить список с идентификаторами и названиями объектов
        /// </summary>
        /// <returns>Список объектов</returns>
        public List<object> GetSportObjectIds();
    }
}
