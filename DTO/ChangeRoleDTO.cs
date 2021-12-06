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
        /// Название роли
        /// </summary>
        public string RoleName { get; set; }
    }
}
