using System.ComponentModel.DataAnnotations;
namespace PortalLayers.Models
{

    public class Genres
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string? name { get; set; }
        public ICollection<Songs>? songs { get; set; }
    }
}
