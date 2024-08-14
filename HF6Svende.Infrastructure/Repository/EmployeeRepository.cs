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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DemmacsWatchesDbContext _context;

        public EmployeeRepository(DemmacsWatchesDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.Entry(employee).Reference(l => l.PostalCode).LoadAsync();
                await _context.Entry(employee).Reference(l => l.Country).LoadAsync();
                await _context.SaveChangesAsync();
                return employee;
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
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return false;
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the employee.", ex);
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _context.Employees.Include(l => l.PostalCode).Include(l => l.Country).Include(l => l.Role).Include(l => l.Department).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the employees.", ex);
            }
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            try
            {
                return await _context.Employees.Include(l => l.PostalCode).Include(l => l.Country).Include(l => l.Role).Include(l => l.Department)
                    .FirstOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the employee.", ex);
            }
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the employee.", ex);
            }
        }
    }
}
