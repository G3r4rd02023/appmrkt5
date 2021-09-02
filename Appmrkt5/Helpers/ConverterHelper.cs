using Appmrkt5.Data;
using Appmrkt5.Data.Entities;
using Appmrkt5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appmrkt5.Helpers
{
    public class ConverterHelper:IConverterHelper
    {
        //private readonly DataContext _context;

        //public ConverterHelper(DataContext context)
        //{
        //    _context = context;
        //}

        
        public Country ToCountry(CountryViewModel model, bool isNew, string path)
        {
            return new Country
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                Name = model.Name
            };
        }


        public CountryViewModel ToCountryViewModel(Country country)
        {
            return new CountryViewModel
            {
                Id = country.Id,
                ImageUrl = country.ImageUrl,
                Name = country.Name
            };
        }
    }
}
