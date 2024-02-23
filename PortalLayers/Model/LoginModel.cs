using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalLayers.DAL.Entities
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Логин или пароль неверный")]
        [Display(Name = "Введите логин: ")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Пароль или логин неверный")]
        [Display(Name = "Введите пароль: ")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}



