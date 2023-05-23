using ASUSport.Models;
using ASUSport.DTO;
using System.Collections.Generic;

namespace ASUSport.Repositories.Impl
{
    /// <summary>
    /// Репозиторий событий
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// Регистрация пользователя на событие
        /// </summary>
        /// <param name="eventId">Идентификатор события</param>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Ответ</returns>
        public Response SignUpForAnEvent(int eventId, string login);

        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <param name="data">Форма для ввода праметров события</param>
        /// <returns>Ответ</returns>
        public Response AddEvent(EventDTO data);

        /// <summary>
        /// Поиск тренера по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор тренера</param>
        /// <returns></returns>
        public User GetTrainer(int id);

        /// <summary>
        /// Получить список событий по параметрам
        /// </summary>
        /// <param name="section">Идентификатор секции</param>
        /// <param name="trainer">Идентификатор тренера</param>
        /// <param name="date">Дата</param>
        /// <param name="time">Время</param>
        /// <returns>Список событий</returns>
        public List<EventModelDTO> GetEvents(int? section, int? trainer, string date, string time);

        /// <summary>
        /// Получить идентификатор события по параметрам
        /// </summary>
        /// <param name="section">Идентификатор секции</param>
        /// <param name="trainer">Идентификатор тренера</param>
        /// <param name="date">Дата</param>
        /// <param name="time">Время</param>
        /// <returns>Идентификатор события</returns>
        public int GetEvent(int? section, int? trainer, string date, string time);


        /// <summary>
        /// Получить для конкретной даты и идентификатора объекта список событий
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <param name="date">Дата</param>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Список событий</returns>
        public EventsForSportobjectDTO GetEventByDateSportObject(int id, string date, string login);

        /// <summary>
        /// Запись на событие неавторизованного пользователя
        /// </summary>
        /// <param name="data">Идентификатор события, имя и e-mail клиента</param>
        /// <returns></returns>
        public Response SignUpForUnathorized(SignUpForUnathorizedDTO data);

        /// <summary>
        /// Отменить регистрацию на событие для пользователя
        /// </summary>
        /// <param name="id">Идентификатор события</param>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        public Response UnsubscribeForTheEvent(int id, string login);

        /// <summary>
        /// Изменить параметры события
        /// </summary>
        /// <param name="data">Форма для изменения данных события</param>
        /// <returns></returns>
        public Response UpdateEvent(UpdateEventDTO data);

        /// <summary>
        /// Удалить событие по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response DeleteEvent(int id);

        /// <summary>
        /// Получить список событий с клиентами для даты и спортивного объекта
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="sportObject">Идентификатор спортивного объекта</param>
        /// <returns></returns>
        public byte[] GetEventsWithClients(string date, int sportObject);

        /// <summary>
        /// Обновление таблицы событий
        /// </summary>
        /// <param name="data">Табличные данные</param>
        /// <returns></returns>
        public Response UpdateEvents(List<UpdateEventTableDTO> data);

        /// <summary>
        /// Получить количестов строк в таблице
        /// </summary>
        /// <returns> Количество строк</returns>
        public int GetNumberOfEntities();

        /// <summary>
        /// Получить список всех событий, как они хранятся в БД
        /// </summary>
        /// <returns></returns>
        public List<UpdateEventTableDTO> GetTableData();
    }
}
