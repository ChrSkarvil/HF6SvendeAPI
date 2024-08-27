using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO;
using HF6Svende.Application.DTO.Login;
using HF6Svende.Application.DTO.Product;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Entities;
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
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;


        public LoginService(ILoginRepository loginRepository, ICustomerRepository customerRepository, IEmployeeRepository employeeRepository,
            IRoleRepository roleRepository, IRefreshTokenRepository refreshTokenRepository, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _loginRepository = loginRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public async Task<AuthResponse> AuthenticateUserAsync(LoginAuthDTO loginDto)
        {
            try
            {
                // Get the login details
                var login = await _loginRepository.GetLoginByEmailAsync(loginDto.Email);

                if (login == null || !BC.Verify(loginDto.Password, login.Password))
                {
                    throw new UnauthorizedAccessException("Invalid credentials.");
                }

                // Map to LoginDTO to get the role
                var loginDtoWithRole = _mapper.Map<LoginDTO>(login);

                var user = new LoginDTO
                {
                    Email = loginDtoWithRole.Email,
                    FullName = loginDtoWithRole.FullName,
                    Role = loginDtoWithRole.Role,
                    CustomerId = login.CustomerId,
                    EmployeeId = login.EmployeeId
                };

                // Generate JWT token using the role from loginDtoWithRole
                var token = _jwtTokenService.GenerateJwtToken(
                    loginDtoWithRole.Email,
                    loginDtoWithRole.Role,
                    loginDtoWithRole.FullName,
                    loginDtoWithRole.Id.ToString(),
                    login.CustomerId
                    );

                // Generate refresh token
                var refreshToken = _jwtTokenService.GenerateRefreshToken();

                // Save refresh token to database
                var refreshTokenEntity = new RefreshToken
                {
                    LoginId = login.Id,
                    Token = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddHours(24) // Refresh token valid for 24 hours
                };

                await _refreshTokenRepository.CreateRefreshTokenAsync(refreshTokenEntity);



                return new AuthResponse
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    User = user
                };
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while authenticating the user.", ex);
            }
        }

        public async Task<AuthResponse> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = _jwtTokenService.ValidateToken(token);
            var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (email == null)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }

            var login = await _loginRepository.GetLoginByEmailAsync(email);
            if (login == null)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }

            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (storedRefreshToken == null || storedRefreshToken.LoginId != login.Id || storedRefreshToken.ExpiresAt <= DateTime.UtcNow || storedRefreshToken.RevokedAt != null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            var loginDtoWithRole = _mapper.Map<LoginDTO>(login);


            // Generate new JWT token
            var newToken = _jwtTokenService.GenerateJwtToken(
                loginDtoWithRole.Email,
                loginDtoWithRole.Role,
                loginDtoWithRole.FullName,
                loginDtoWithRole.Id.ToString(),
                login.CustomerId
            );

            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

            // Revoke old refresh token
            storedRefreshToken.RevokedAt = DateTime.UtcNow;
            await _refreshTokenRepository.UpdateRefreshTokenAsync(storedRefreshToken);

            // Create new refresh token
            var refreshTokenEntity = new RefreshToken
            {
                LoginId = login.Id,
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.CreateRefreshTokenAsync(refreshTokenEntity);

            return new AuthResponse
            {
                Token = newToken,
                RefreshToken = newRefreshToken,
                User = _mapper.Map<LoginDTO>(login)
            };
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

        public async Task<int> GetLoginsCountAsync()
        {
            try
            {
                return await _loginRepository.GetLoginsCountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the login count.", ex);
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
