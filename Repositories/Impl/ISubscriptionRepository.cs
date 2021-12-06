using ASUSport.Models;
using ASUSport.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public List<SubscriptionDTO> GetSubscriptions(int objectId);
    }
}
