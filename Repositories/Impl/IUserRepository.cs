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
        public Response UpdateUserData(UserDataDTO data, string login);

        /// <summary>
        /// Изменить роль пользователя по логину
        /// </summary>
        /// <param name="data">Данные из формы</param>
        /// <returns></returns>
        public Response ChangeRole(ChangeRoleDTO data);

        /// <summary>
        /// Получить список всех пользователей или определенной роли
        /// </summary>
        /// /// <param name="data">Роль пользователей</param>
        /// <returns>Список пользователей</returns>
        public List<UserInfoDTO> GetUsers(string role);

        /// <summary>
        /// Сохранение данных пользователя при регистрации
        /// </summary>
        /// <param name="data">Данные пользователя</param>
        public void SaveUserData(UserData data);

        /// <summary>
        /// Обновление таблиц с данными пользователей
        /// </summary>
        /// <param name="data">Табличные данные</param>
        /// <returns></returns>
        public Response UpdateUsers(List<UserInfoDTO> data);

        /// <summary>
        /// Получить количество админов
        /// </summary>
        /// <returns>Количество админов</returns>
        public int GetNumberOfAdmins();

        /// <summary>
        /// Получить количество клиентов
        /// </summary>
        /// <returns>Количество клиентов</returns>
        public int GetNumberOfClients();

        /// <summary>
        /// Получить количество тренеров
        /// </summary>
        /// <returns>Количество тренеров</returns>
        public int GetNumberOfTrainers();
    }
}
