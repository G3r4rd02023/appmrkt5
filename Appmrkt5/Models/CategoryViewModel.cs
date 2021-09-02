using Appmrkt5.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appmrkt5.Models
{
    public class CategoryViewModel : Category
    {
        [Display(Name = "Imagen")]
        public IFormFile ImageFile { get; set; }
    }

}
