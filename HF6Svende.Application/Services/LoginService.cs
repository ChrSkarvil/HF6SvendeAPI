﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO.Login;
using HF6Svende.Application.DTO.Product;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Interfaces;
using HF6Svende.Core.Repository_Interfaces;
using HF6Svende.Infrastructure.Repository;
using HF6SvendeAPI.Data.Entities;
using BC = BCrypt.Net.BCrypt;


namespace HF6Svende.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;


        public LoginService(ILoginRepository loginRepository, ICustomerRepository customerRepository, IEmployeeRepository employeeRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _loginRepository = loginRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<LoginDTO> CreateLoginAsync(LoginCreateDTO createLoginDto)
        {
            try
            {
                // Mapping dto to entity
                var login = _mapper.Map<Login>(createLoginDto);

                // Determine if it's a Customer or Employee
                if (login.CustomerId.HasValue)
                {
                    var customer = await _customerRepository.GetCustomerByIdAsync(login.CustomerId.Value);
                    if (customer == null)
                    {
                        throw new Exception("Customer not found.");
                    }
                }
                else if (login.EmployeeId.HasValue)
                {
                    var employee = await _employeeRepository.GetEmployeeByIdAsync(login.EmployeeId.Value);
                    if (employee == null)
                    {
                        throw new Exception("Employee not found.");
                    }
                }
                else
                {
                    throw new Exception("Login must be associated with either a Customer or an Employee.");
                }

                //Hash the password
                login.Password = BC.HashPassword(createLoginDto.Password);

                // Create login in repository
                var createdLogin = await _loginRepository.CreateLoginAsync(login);

                // Mapping back to dto
                return _mapper.Map<LoginDTO>(createdLogin);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the login.", ex);
            }
        }

        public async Task<bool> DeleteLoginAsync(int id)
        {
            try
            {
                // Delete login
                var success = await _loginRepository.DeleteLoginAsync(id);
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the login.", ex);
            }
        }

        public async Task<List<LoginDTO>> GetAllLoginsAsync()
        {
            try
            {
                // Get all logins
                var logins = await _loginRepository.GetAllLoginsAsync();

                // Mapping back to dto
                return _mapper.Map<List<LoginDTO>>(logins);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the logins.", ex);
            }

        }

        public async Task<LoginDTO?> GetLoginByEmailAsync(string mail)
        {
            try
            {
                // Get login by email
                var login = await _loginRepository.GetLoginByEmailAsync(mail);

                if (login == null) return null;

                // Mapping back to dto
                return _mapper.Map<LoginDTO>(login);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the login.", ex);
            }
        }

        public async Task<LoginDTO?> GetLoginByIdAsync(int id)
        {
            try
            {
                // Get login by id
                var login = await _loginRepository.GetLoginByIdAsync(id);

                if (login == null) return null;

                // Mapping back to dto
                return _mapper.Map<LoginDTO>(login);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the login.", ex);
            }
        }

        public async Task<LoginDTO?> UpdateLoginAsync(int id, LoginUpdateDTO updateLoginDto)
        {
            try
            {
                // Get existing login
                var login = await _loginRepository.GetLoginByIdAsync(id);
                if (login == null) return null;

                // Mapping dto to entity
                _mapper.Map(updateLoginDto, login);

                // Save the changes in the repository
                var updatedLogin = await _loginRepository.UpdateLoginAsync(login);

                // Mapping back to dto
                return _mapper.Map<LoginDTO>(updatedLogin);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the login.", ex);
            }
        }
    }
}
