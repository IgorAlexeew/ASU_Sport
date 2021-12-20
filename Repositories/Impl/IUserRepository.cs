using ASUSport.Models;
using ASUSport.DTO;
using System.Collections.Generic;

namespace ASUSport.Repositories.Impl
{
    /// <summary>
    /// Репозиторий пользователей
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получить данные авторизованного пользователя по логину
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns></returns>
        public UserModelDTO GetUserInfo(string login);

        /// <summary>
        /// Проверка на наличие пользователя с таким логином в БД
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Наличие пользователя</returns>
        public bool IsContains(string login);

        /// <summary>
        /// Поиск пользователя по коду доступа
        /// </summary>
        /// <param name="loginHash">хеш логина</param>
        /// <returns>Пользователь или null</returns>
        public User GetAdminByHash(string loginHash);

        /// <summary>
        /// Поиск пользователя по логину и паролю
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Пользователь</returns>
        public User GetUserByLoginPassword(string login, string password);

        /// <summary>
        /// Поиск в БД роли клиента
        /// </summary>
        /// <returns>Роль клиента</returns>
        public Role GetClientRole();

        /// <summary>
        /// Сохранение нового пользователя в БД
        /// </summary>
        /// <param name="user">Пользователь</param>
        public void Save(User user);

        /// <summary>
        /// Изменение данных пользователя по логину
        /// </summary>
        /// <param name="data">Данные из формы</param>
        /// <param name="login">Логин</param>
        /// <returns>Ответ</returns>
        public Response EditUserData(UserDataDTO data, string login);

        /// <summary>
        /// Изменить роль пользователя по логину
        /// </summary>
        /// <param name="data">Данные из формы</param>
        /// <returns></returns>
        public Response ChangeRole(ChangeRoleDTO data);

        /// <summary>
        /// Получить список тренеров
        /// </summary>
        /// <returns>Список тренеров</returns>
        public List<TrainerDTO> GetTrainers();

        /// <summary>
        /// Сохранение данных пользователя при регистрации
        /// </summary>
        /// <param name="data">Данные пользователя</param>
        /// <returns></returns>
        public Response SaveUserData(UserData data);
    }
}
