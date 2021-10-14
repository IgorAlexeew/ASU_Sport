using System.ComponentModel.DataAnnotations;

namespace ASUSport.ViewModels
{
    public class LoginModel
    {
        [Required]/*(ErrorMessage = "Не указан логин")]*/
        public string Login { get; set; }

        [Required]
        /*[DataType(DataType.Password)]*/
        public string Password { get; set; }
    }
}
