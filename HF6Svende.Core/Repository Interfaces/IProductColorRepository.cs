using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Core.Repository_Interfaces
{
    public interface IProductColorRepository
    {
        Task<List<ProductColor>> GetAllProductColorsAsync();
        Task<ProductColor?> GetProductColorByIdAsync(int id);
        Task<ProductColor?> GetProductColorByNameAsync(string color);
        Task<ProductColor> CreateProductColorAsync(ProductColor productColor);
        Task<bool> DeleteProductColorAsync(int id);

    }
}
