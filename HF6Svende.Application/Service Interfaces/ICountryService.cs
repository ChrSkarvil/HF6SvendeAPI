using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Country;
using HF6Svende.Application.DTO.Customer;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryDTO>> GetAllCountriesAsync();
        Task<CountryDTO?> GetCountryByNameAsync(string countryName);
        Task<CountryDTO?> GetCountryByIdAsync(int id);
    }
}
