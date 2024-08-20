using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO.Employee;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Repository_Interfaces;
using HF6Svende.Infrastructure.Repository;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;


        public EmployeeService(IEmployeeRepository employeeRepository, IPostalCodeRepository postalCodeRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _postalCodeRepository = postalCodeRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> CreateEmployeeAsync(EmployeeCreateDTO createEmployeeDto)
        {
            try
            {
                //Look up the postal code in the repository
                var postalCode = await _postalCodeRepository.GetPostalCodeByCodeAsync(createEmployeeDto.PostalCode);
                if (postalCode == null)
                {
                    throw new Exception("Postal code not found.");
                }

                // Set the PostalCodeId to the found postal code's ID
                createEmployeeDto.PostalCodeId = postalCode.Id;

                //Look up the country in the repository
                var country = await _countryRepository.GetCountryByNameAsync(createEmployeeDto.CountryName);
                if (country == null)
                {
                    throw new Exception("Country not found.");
                }

                // Set the CountryId to the found country's ID
                createEmployeeDto.CountryId = country.Id;

                // Mapping dto to entity
                var employee = _mapper.Map<Employee>(createEmployeeDto);

                // Create employee in repository
                var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);

                // Mapping back to dto
                return _mapper.Map<EmployeeDTO>(createdEmployee);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the employee.", ex);
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                // Delete employee
                var success = await _employeeRepository.DeleteEmployeeAsync(id);
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the employee.", ex);
            }
        }

        public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
        {
            try
            {
                // Get all employees
                var employees = await _employeeRepository.GetAllEmployeesAsync();

                // Mapping back to dto
                return _mapper.Map<List<EmployeeDTO>>(employees);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the employee.", ex);
            }
        }

        public async Task<EmployeeDTO?> GetEmployeeByIdAsync(int id)
        {
            try
            {
                // Get employee by id
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

                if (employee == null) return null;

                // Mapping back to dto
                return _mapper.Map<EmployeeDTO>(employee);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the employee.", ex);
            }
        }

        public async Task<EmployeeDTO?> UpdateEmployeeAsync(int id, EmployeeUpdateDTO updateEmployeeDto)
        {
            try
            {
                // Get existing employee
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null) return null;

                //Look up the postal code in the repository
                var postalCode = await _postalCodeRepository.GetPostalCodeByCodeAsync(updateEmployeeDto.PostalCode);
                if (postalCode == null)
                {
                    throw new Exception("Postal code not found.");
                }

                // Set the PostalCodeId to the found postal code's ID
                updateEmployeeDto.PostalCodeId = postalCode.Id;

                //Look up the country in the repository
                var country = await _countryRepository.GetCountryByNameAsync(updateEmployeeDto.CountryName);
                if (country == null)
                {
                    throw new Exception("Country not found.");
                }

                // Set the CountryId to the found country's ID
                updateEmployeeDto.CountryId = country.Id;

                // Mapping dto to entity
                _mapper.Map(updateEmployeeDto, employee);

                // Save the changes in the repository
                var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee);

                // Mapping back to dto
                return _mapper.Map<EmployeeDTO>(updatedEmployee);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the employee.", ex);
            }
        }
    }
}
