using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASUSport.DTO
{
    /// <summary>
    /// Форма для изменения роли
    /// </summary>
    public class ChangeRoleDTO
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int Role { get; set; }
    }
}
