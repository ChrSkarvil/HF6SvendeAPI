using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Core.Repository_Interfaces;
using HF6SvendeAPI.Data;
using HF6SvendeAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HF6Svende.Infrastructure.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DemmacsWatchesDbContext _context;

        public LoginRepository(DemmacsWatchesDbContext context)
        {
            _context = context;
        }

        public async Task<Login> CreateLoginAsync(Login login)
        {
            try
            {
                _context.Logins.Add(login);
                await _context.Entry(login).Reference(l => l.Customer).LoadAsync();
                await _context.Entry(login).Reference(l => l.Employee).LoadAsync();
                await _context.SaveChangesAsync();
                return login;
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
                var login = await _context.Logins.FindAsync(id);
                if (login == null)
                {
                    return false;
                }

                _context.Logins.Remove(login);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the login.", ex);
            }
        }

        public async Task<List<Login>> GetAllLoginsAsync()
        {
            try
            {
                return await _context.Logins.Include(l => l.Customer).Include(l => l.Employee).ThenInclude(e => e.Role).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the logins.", ex);
            }
        }

        public async Task<Login?> GetLoginByEmailAsync(string mail)
        {
            try
            {
                return await _context.Logins.Include(l => l.Customer).Include(l => l.Employee).ThenInclude(e => e.Role).FirstOrDefaultAsync(l => l.Email == mail);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the login.", ex);
            }
        }

        public async Task<Login?> GetLoginByIdAsync(int id)
        {
            try
            {
                return await _context.Logins.Include(l => l.Customer).Include(l => l.Employee).ThenInclude(e => e.Role).FirstOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the login.", ex);
            }
        }

        public async Task<Login> UpdateLoginAsync(Login login)
        {
            try
            {
                _context.Logins.Update(login);
                await _context.SaveChangesAsync();
                return login;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the login.", ex);
            }
        }
    }
}
