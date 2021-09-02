using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appmrkt5.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "País")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }

        [Display(Name = "Foto")]
        public string ImageUrl { get; set; }

        // TODO: Change the path when publish
        [Display(Name = "Foto")]
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? $"https://localhost:44304/images/noimage.png"
            : $"https://localhost:44304{ImageUrl[1..]}";
        //: $"https://workshopvehicles.azurewebsites.net{ImageUrl.Substring(1)}";

        [DisplayName("#Ciudades")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}
