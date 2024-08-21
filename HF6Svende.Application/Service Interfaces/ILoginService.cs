using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO;
using HF6Svende.Application.DTO.Login;
using HF6Svende.Application.DTO.Product;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface ILoginService
    {
        Task<List<LoginDTO>> GetAllLoginsAsync();
        Task<LoginDTO?> GetLoginByIdAsync(int id);
        Task<LoginDTO?> GetLoginByEmailAsync(string mail);
        Task<LoginDTO> CreateLoginAsync(LoginCreateDTO createLoginDto);
        Task<LoginDTO?> UpdateLoginAsync(int id, LoginUpdateDTO updateLoginDto);
        Task<AuthResponse> AuthenticateUserAsync(LoginAuthDTO loginDto);
        Task<bool> DeleteLoginAsync(int id);
    }
}
