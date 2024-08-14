using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Core.Repository_Interfaces
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAllCountriesAsync();
        Task<Country?> GetCountryByNameAsync(string countryName);
        Task<Country?> GetCountryByIdAsync(int id);

    }
}
