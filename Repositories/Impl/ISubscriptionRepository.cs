using ASUSport.DTO;
using ASUSport.Models;
using System.Collections.Generic;

namespace ASUSport.Repositories.Impl
{
    /// <summary>
    /// Репозиторий абонементов
    /// </summary>
    public interface ISubscriptionRepository
    {
        /// <summary>
        /// Получить список абонементов для конкретного спортивного объекта
        /// </summary>
        /// <param name="objectId">Идентификатор ообъекта</param>
        /// <returns>Список абонементов</returns>
        public List<SubscriptionDTO> GetSubscriptions(int? objectId);

        /// <summary>
        /// Добавление нового абонемента
        /// </summary>
        /// <param name="data">Параметры абонемента</param>
        /// <returns></returns>
        public Response AddSubscription(SubscriptionDTO data);

        /// <summary>
        /// Изменение данных абонемента
        /// </summary>
        /// <param name="data">Идентификатор и параметры абонемента</param>
        /// <returns></returns>
        public Response UpdateSubscription(UpdateSubscriptionDTO data);

        /// <summary>
        /// Удаление абонемента по идентификатору
        /// </summary>
        /// <param name="id">Идентифкатор абонемента</param>
        /// <returns></returns>
        public Response DeleteSubscription(int id);

        /// <summary>
        /// Обновление таблицы с абонементами
        /// </summary>
        /// <param name="data">Табличные данные</param>
        /// <returns></returns>
        public Response UpdateTable(List<UpdateSubscriptionDTO> data);

        /// <summary>
        /// Получить количестов строк в таблице
        /// </summary>
        /// <returns> Количество строк</returns>
        public int GetNumberOfEntities();
    }
}
