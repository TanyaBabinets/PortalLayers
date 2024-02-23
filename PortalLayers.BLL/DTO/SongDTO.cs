
using PortalLayers.Models;
using System.ComponentModel.DataAnnotations;

namespace PortalLayers.BLL.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните поле")]
        [Display(Name = "Название")]
        public string? name { get; set; }
        [Required(ErrorMessage = "Заполните поле")]
        [Display(Name = "Исполнитель")]
        public string? singer { get; set; }
        [Display(Name = "Длительность")]
        public string? runtime { get; set; }
        [Display(Name = "Размер файла в МБ")]
        public double? size { get; set; }
        [Display(Name = "Музыкальный файл")]
        //[Required(ErrorMessage = "Загрузите файл в формате мр3 ")]
        public string? file { get; set; }
        //[Required(ErrorMessage = "Загрузите файл в формате .jpg ")]
        [Display(Name = "Постер")]
        public string? pic { get; set; }
        [Display(Name = "Дата загрузки")]
        public DateTime? dateTime { get; set; }
      
        public int? userId { get; set; }
		[Display(Name = "Загружено пользователем:")]
		public UserDTO? user { get; set; }
        [Display(Name = "Жанр:")]
        public int? genreId { get; set; }
		[Display(Name = "Жанр:")]
		public string? genre { get; set; }

    }
}

