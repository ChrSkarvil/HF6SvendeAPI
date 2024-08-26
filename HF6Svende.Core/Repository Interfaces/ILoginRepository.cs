using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Core.Repository_Interfaces
{
    public interface ILoginRepository
    {
        Task<List<Login>> GetAllLoginsAsync();
        Task<Login?> GetLoginByIdAsync(int id);
        Task<Login?> GetLoginByEmailAsync(string mail);
        Task<Login> CreateLoginAsync(Login login);
        Task<Login> UpdateLoginAsync(Login login);
        Task<bool> DeleteLoginAsync(int id);

        Task<int> GetLoginsCountAsync();

    }
}
