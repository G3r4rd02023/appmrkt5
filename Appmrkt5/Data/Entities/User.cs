using Appmrkt5.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appmrkt5.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Identidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public string Document { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Dirección")]
        [DataType(DataType.MultilineText)]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        public string Address { get; set; }

        [Display(Name = "Foto")]
        public string ImageUrl { get; set; }

        // TODO: Change the path when publish
        [Display(Name = "Foto")]
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? $"https://localhost:44304/images/noimage.png"
            : $"https://localhost:44304{ImageUrl[1..]}";
            //: $"https://workshopvehicles.azurewebsites.net{ImageUrl.Substring(1)}";

        [Display(Name = "Rol")]
        public UserType UserType { get; set; }

        public City City { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        
    }
}
