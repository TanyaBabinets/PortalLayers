using System;
using System.ComponentModel.DataAnnotations;


namespace PortalLayers.BLL.DTO
{

    public class GenreDTO
        {
            public int Id { get; set; }
            [Display(Name = "Название")]
            public string? name { get; set; }
   
    
}
    }

