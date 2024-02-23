using System.ComponentModel.DataAnnotations;


namespace PortalLayers.BLL.DTO
{


    public class UserDTO
    {
        public int Id { get; set; }
        [Display(Name = "Имя: ")]
        public string? FirstName { get; set; }
        [Display(Name = "Фамилия: ")]
        public string? LastName { get; set; }
        [Display(Name = "Логин: ")]
        public string? Login { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Salt { get; set; }
        [Display(Name = "Одобрен: ")]
        public bool IsActivated { get; set; }

       
    }
}
