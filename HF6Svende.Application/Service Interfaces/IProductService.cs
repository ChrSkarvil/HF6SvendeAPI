using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Product;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task<ProductDTO> CreateProductAsync(ProductCreateDTO createProductDto);
        Task<ProductDTO?> UpdateProductAsync(int id, ProductUpdateDTO updateProductDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
