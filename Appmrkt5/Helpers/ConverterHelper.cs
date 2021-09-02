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
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context,ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public Category ToCategory(CategoryViewModel model, string path, bool isNew)
        {
            return new Category
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                Name = model.Name
            };
        }

        public CategoryViewModel ToCategoryViewModel(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                ImageUrl = category.ImageUrl,
                Name = category.Name
            };
        }

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

        public async Task<Product> ToProductAsync(ProductViewModel model, bool isNew, string path)
        {
            return new Product
            {
                Category = await _context.Categories.FindAsync(model.CategoryId),                
                Description = model.Description,
                Id = isNew ? 0 : model.Id,
                IsActive = model.IsActive,
                IsStarred = model.IsStarred,
                Name = model.Name,
                Price = model.Price,
                ProductImages = model.ProductImages

            };
        }

        public ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Categories = _combosHelper.GetComboCategories(),
                Category = product.Category,
                CategoryId = product.Category.Id,
                Description = product.Description,
                Id = product.Id,
                IsActive = product.IsActive,
                IsStarred = product.IsStarred,
                Name = product.Name,
                Price = product.Price,
                ProductImages = product.ProductImages
            };
        }
    }
}
