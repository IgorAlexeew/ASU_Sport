using ASUSport.Models;
using ASUSport.DTO;

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
        /// Присвоение пользователю роли в соответствии с кодом доступа
        /// </summary>
        /// <param name="accessCode">Код доступа</param>
        /// <returns>Роль</returns>
        public Role SetRole(string accessCode);

        /// <summary>
        /// Сохранение нового пользователя в БД
        /// </summary>
        /// <param name="user">Пользователь</param>
        public void Save(User user);

        /// <summary>
        /// Добавление данных пользователя по логину
        /// </summary>
        /// <param name="data">Данные из формы</param>
        /// <param name="login">Логин</param>
        /// /// <returns>Ответ</returns>
        public Response AddUserData(UserModelDTO data, string login);

    }
}
