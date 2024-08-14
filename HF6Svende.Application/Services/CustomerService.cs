using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO.Customer;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Interfaces;
using HF6Svende.Core.Repository_Interfaces;
using HF6Svende.Infrastructure.Repository;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;


        public CustomerService(ICustomerRepository customerRepository, IPostalCodeRepository postalCodeRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _postalCodeRepository = postalCodeRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<CustomerDTO> CreateCustomerAsync(CustomerCreateDTO createCustomerDto)
        {
            try
            {
                //Look up the postal code in the repository
                var postalCode = await _postalCodeRepository.GetPostalCodeByCodeAsync(createCustomerDto.PostalCode);
                if (postalCode == null)
                {
                    throw new Exception("Postal code not found.");
                }

                // Set the PostalCodeId to the found postal code's ID
                createCustomerDto.PostalCodeId = postalCode.Id;

                //Look up the country in the repository
                var country = await _countryRepository.GetCountryByNameAsync(createCustomerDto.CountryName);
                if (country == null)
                {
                    throw new Exception("Country not found.");
                }

                // Set the CountryId to the found country's ID
                createCustomerDto.CountryId = country.Id;

                // Mapping dto to entity
                var customer = _mapper.Map<Customer>(createCustomerDto);

                // Create customer in repository
                var createdCustomer = await _customerRepository.CreateCustomerAsync(customer);

                // Mapping back to dto
                return _mapper.Map<CustomerDTO>(createdCustomer);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the customer.", ex);
            }
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                // Delete customer
                var success = await _customerRepository.DeleteCustomerAsync(id);
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the customer.", ex);
            }
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            try
            {
                // Get all customers
                var customers = await _customerRepository.GetAllCustomersAsync();

                // Mapping back to dto
                return _mapper.Map<List<CustomerDTO>>(customers);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the customer.", ex);
            }
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int id)
        {
            try
            {
                // Get customer by id
                var customer = await _customerRepository.GetCustomerByIdAsync(id);

                if (customer == null) return null;

                // Mapping back to dto
                return _mapper.Map<CustomerDTO>(customer);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the customer.", ex);
            }
        }

        public async Task<CustomerDTO> UpdateCustomerAsync(int id, CustomerUpdateDTO updateCustomerDto)
        {
            try
            {
                // Get existing customer
                var customer = await _customerRepository.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    throw new Exception("Customer not found.");
                }

                //Look up the postal code in the repository
                var postalCode = await _postalCodeRepository.GetPostalCodeByCodeAsync(updateCustomerDto.PostalCode);
                if (postalCode == null)
                {
                    throw new Exception("Postal code not found.");
                }

                // Set the PostalCodeId to the found postal code's ID
                updateCustomerDto.PostalCodeId = postalCode.Id;

                //Look up the country in the repository
                var country = await _countryRepository.GetCountryByNameAsync(updateCustomerDto.CountryName);
                if (country == null)
                {
                    throw new Exception("Country not found.");
                }

                // Set the CountryId to the found country's ID
                updateCustomerDto.CountryId = country.Id;

                // Mapping dto to entity
                _mapper.Map(updateCustomerDto, customer);

                // Save the changes in the repository
                var updatedCustomer = await _customerRepository.UpdateCustomerAsync(customer);

                // Mapping back to dto
                return _mapper.Map<CustomerDTO>(updatedCustomer);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the customer.", ex);
            }
        }
    }
}
