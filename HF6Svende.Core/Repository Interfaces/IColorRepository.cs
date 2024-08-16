using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Core.Repository_Interfaces
{
    public interface IColorRepository
    {
        Task<List<Color>> GetAllColorsAsync();
        Task<Color?> GetColorByIdAsync(int id);
        Task<Color?> GetColorByNameAsync(string color);
    }
}
