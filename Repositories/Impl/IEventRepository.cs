using ASUSport.Models;
using ASUSport.ViewModels;

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
        public void SignUpForAnEvent(EventDTO data, string login);

        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <param name="data">Форма для ввода праметров события</param>
        public void AddEvent(EventDTO data);

        /// <summary>
        /// Поиск тренера по ФИО
        /// </summary>
        /// <param name="fullName">ФИО тренера</param>
        /// <returns></returns>
        public User GetTrainer(string fullName);
    }
}
