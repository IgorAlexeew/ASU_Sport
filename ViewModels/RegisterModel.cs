using System.ComponentModel.DataAnnotations;

namespace ASUSport.ViewModels
{
    public class RegisterModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string AccessCode { get; set; }
    }
}
