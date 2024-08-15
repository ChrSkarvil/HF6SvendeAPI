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
    public class RoleRepository : IRoleRepository
    {
        private readonly DemmacsWatchesDbContext _context;

        public RoleRepository(DemmacsWatchesDbContext context)
        {
            _context = context;
        }
        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            try
            {
                return await _context.Roles.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the role.", ex);
            }
        }
    }
}
