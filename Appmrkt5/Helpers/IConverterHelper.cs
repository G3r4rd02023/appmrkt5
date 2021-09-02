using Appmrkt5.Data.Entities;
using Appmrkt5.Models;

namespace Appmrkt5.Helpers
{
    public interface IConverterHelper
    {

        Country ToCountry(CountryViewModel model, bool isNew, string path);

        CountryViewModel ToCountryViewModel(Country country);


    }
}
