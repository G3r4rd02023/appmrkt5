using Appmrkt5.Data.Entities;
using Appmrkt5.Models;
using System.Threading.Tasks;

namespace Appmrkt5.Helpers
{
    public interface IConverterHelper
    {

        Country ToCountry(CountryViewModel model, bool isNew, string path);

        CountryViewModel ToCountryViewModel(Country country);

        Category ToCategory(CategoryViewModel model, string path, bool isNew);

        CategoryViewModel ToCategoryViewModel(Category category);

        Task<Product> ToProductAsync(ProductViewModel model, bool isNew, string path);

        ProductViewModel ToProductViewModel(Product product);


    }
}
