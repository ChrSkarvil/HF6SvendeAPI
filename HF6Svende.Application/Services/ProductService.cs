using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO.Product;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Interfaces;
using HF6Svende.Core.Repository_Interfaces;
using HF6Svende.Infrastructure.Repository;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductDTO> CreateProductAsync(ProductCreateDTO createProductDto)
        {
            try
            {
                // Mapping dto to entity
                var product = _mapper.Map<Product>(createProductDto);

                // Create product in repository
                var createdProduct = await _productRepository.CreateProductAsync(product);

                // Mapping back to dto
                return _mapper.Map<ProductDTO>(createdProduct);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the product.", ex);
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                // Delete product
                var success = await _productRepository.DeleteProductAsync(id);
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the product.", ex);
            }
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            try
            {

                // Get all products
                var products = await _productRepository.GetAllProductsAsync();

                // Mapping back to dto
                return _mapper.Map<List<ProductDTO>>(products);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the product.", ex);
            }
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            try
            {
                // Get product by id
                var product = await _productRepository.GetProductByIdAsync(id);

                if (product == null) return null;

                // Mapping back to dto
                return _mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the product.", ex);
            }
        }

        public async Task<ProductDTO?> UpdateProductAsync(int id, ProductUpdateDTO updateProductDto)
        {
            try
            {
                // Get existing product
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null) return null;

                // Mapping dto to entity
                _mapper.Map(updateProductDto, product);

                // Save the changes in the repository
                var updatedProduct = await _productRepository.UpdateProductAsync(product);

                // Mapping back to dto
                return _mapper.Map<ProductDTO>(updatedProduct);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the product.", ex);
            }
        }
    }
}
