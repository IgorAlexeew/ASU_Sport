using ASUSport.Models;
using ASUSport.ViewModels;
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
        public Response SignUpForAnEvent(EventDTO data, string login);

        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <param name="data">Форма для ввода праметров события</param>
        /// <returns>Ответ</returns>
        public Response AddEvent(EventDTO data);

        /// <summary>
        /// Поиск тренера по ФИО
        /// </summary>
        /// <param name="fullName">ФИО тренера</param>
        /// <returns></returns>
        public User GetTrainer(string fullName);

        /// <summary>
        /// Получить список событий по параметрам
        /// </summary>
        /// <param name="parametres">Параметры отбора записей</param>
        /// <returns>Список событий</returns>
        public List<EventDTO> GetEvents(EventDTO parametres);
    }
}
