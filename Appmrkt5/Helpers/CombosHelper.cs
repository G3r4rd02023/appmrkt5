using Appmrkt5.Data;
using Appmrkt5.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appmrkt5.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboCategories()
        {
            List<SelectListItem> list = _context.Categories.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
               .OrderBy(t => t.Text)
               .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una categoría...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCities(int countryId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Country country = _context.Countries
                .Include(d => d.Cities)
                .FirstOrDefault(d => d.Id == countryId);
            if (country != null)
            {
                list = country.Cities.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Selecione una Ciudad...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCountries()
        {
            List<SelectListItem> list = _context.Countries.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a country...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboProducts()
        {
            List<SelectListItem> list = _context.Products.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                 .OrderBy(t => t.Text)
                 .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Producto...]",
                Value = "0"
            });

            return list;
        }
    }
}
