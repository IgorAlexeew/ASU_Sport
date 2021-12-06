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
        /// <param name="data">Форма для ввода праметров события</param>
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
        /// <returns>Список событий</returns>
        public List<EventModelDTO> GetEvents(string section, string trainerName, string date, string time);

        /// <summary>
        /// Получить идентификатор события по параметру
        /// </summary>
        /// <returns>Идентификатор события</returns>
        public int GetEvent(string section, string trainer, string date, string time);

    }
}
