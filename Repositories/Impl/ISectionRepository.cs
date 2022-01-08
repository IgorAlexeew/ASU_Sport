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
        public List<SectionInfoDTO> GetSections(string name, string sportobject);

        /// <summary>
        /// Обновление секции
        /// </summary>
        /// <param name="data">Данные из формы</param>
        /// <returns></returns>
        public Response UpdateSection(UpdateSectionDTO data);

        /// <summary>
        /// Удаление секции
        /// </summary>
        /// <param name="id">Идентификатор секции</param>
        /// <returns></returns>
        public Response DeleteSection(int id);
        
        /// <summary>
        /// Обновление таблицы с секциями
        /// </summary>
        /// <param name="data">Табличные данные</param>
        /// <returns></returns>
        public Response UpdateTable(List<UpdateSectionDTO> data);

        /// <summary>
        /// Получить количестов строк в таблице
        /// </summary>
        /// <returns> Количество строк</returns>
        public int GetNumberOfEntities();
    }
}
