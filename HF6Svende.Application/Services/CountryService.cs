using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO.Country;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Repository_Interfaces;
using HF6Svende.Infrastructure.Repository;

namespace HF6Svende.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;


        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<List<CountryDTO>> GetAllCountriesAsync()
        {
            try
            {
                // Get all countries
                var countries = await _countryRepository.GetAllCountriesAsync();

                // Mapping back to dto
                return _mapper.Map<List<CountryDTO>>(countries);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the country.", ex);
            }
        }

        public async Task<CountryDTO?> GetCountryByIdAsync(int id)
        {
            try
            {
                // Get country by id
                var country = await _countryRepository.GetCountryByIdAsync(id);

                if (country == null) return null;

                // Mapping back to dto
                return _mapper.Map<CountryDTO>(country);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the country.", ex);
            }
        }

        public async Task<CountryDTO?> GetCountryByNameAsync(string countryName)
        {
            try
            {
                // Get country by name
                var country = await _countryRepository.GetCountryByNameAsync(countryName);

                if (country == null) return null;

                // Mapping back to dto
                return _mapper.Map<CountryDTO>(country);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the country.", ex);
            }
        }
    }
}
