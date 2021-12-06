﻿using System.ComponentModel.DataAnnotations;

namespace ASUSport.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string AccessCode { get; set; }
    }
}