using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASUSport.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } // имя пользователя
        public int PasswordHash { get; set; } // возраст пользователя
    }
}
