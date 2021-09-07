using Appmrkt5.Data.Entities;
using Appmrkt5.Data.Enums;
using Appmrkt5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appmrkt5.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly Random _random;
        private readonly IConverterHelper _converterHelper;

        public SeedDb(DataContext context, IUserHelper userHelper, IImageHelper imageHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _random = new Random();
            _converterHelper = converterHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("0801-1987-13256", "Gerardo", "Lanza", "glanza007@gmail.com", "3307 7964", "Calle Luna Calle Sol", UserType.Admin);


        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Honduras",
                    Cities = new List<City>
                {
                    new City
                    {
                        Name = "Tegucigalpa",

                    },
                    new City
                    {
                        Name = "San Pedro Sula",
                    },
                    new City
                    {
                        Name = "La Ceiba",
                    }
                }
                });
                _context.Countries.Add(new Country
                {
                    Name = "USA",
                    Cities = new List<City>
                {
                    new City
                    {
                        Name = "Los Angeles",
                    },
                    new City
                    {
                        Name = "Chicago",
                    }
                }
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
