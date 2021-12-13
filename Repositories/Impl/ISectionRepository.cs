using ASUSport.Models;
using ASUSport.DTO;
using System.Collections.Generic;

namespace ASUSport.Repositories.Impl
{
    public interface ISectionRepository
    {
        /// <summary>
        /// Создать новую секцию
        /// </summary>
        /// <param name="data">Данные из формы</param>
        /// <returns>Результат операции</returns>
        public Response AddSection(SectionDTO data);

        /// <summary>
        /// Получить секции по параметрам
        /// </summary>
        /// <param name="name">Название секции</param>
        /// <param name="sportobject">Идентификатор спортивного объекта</param>
        /// <returns>Список секций</returns>
        public List<object> GetSections(string name, string sportobject);
    }
}
