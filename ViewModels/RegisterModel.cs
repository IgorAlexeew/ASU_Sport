using System.ComponentModel.DataAnnotations;

namespace ASUSport.ViewModels
{
    public class RegisterModel
    {
        [Required]//(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Required]//(ErrorMessage = "Не указан пароль")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        public string AccessCode { get; set; }
    }
}
