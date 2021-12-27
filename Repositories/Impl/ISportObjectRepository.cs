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
        /// /// <param name="id">Идентификатор объекта</param>
        /// <returns>Информация о спорт объекте</returns>
        public List<SportObjectForMainDTO> GetInfo(int? id);
        
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

        /// <summary>
        /// Добавление нового спортивного объекта
        /// </summary>
        /// <param name="data">Свойства объекта</param>
        /// <returns></returns>
        public Response AddSportObject(SportObjectDTO data);

        /// <summary>
        /// Обновление существующей записи о спортивном объекте
        /// </summary>
        /// <param name="data">Свойства объекта</param>
        /// <returns></returns>
        public Response UpdateSportObject(SportObjectForUpdateDTO data);

        /// <summary>
        /// Удаление спортивного объекта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        public Response DeleteSportObject(int id);
    }
}
