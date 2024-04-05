using Application.DTOs;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductDto>> CreateProductAsync(CreateProductDto productDto);
        Task<ServiceResponse<IEnumerable<ProductsOfCategoryDto>>> GetAllProductsAsync(int? categoryId);
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int id);
        Task<ServiceResponse<ProductDto>> UpdateProductAsync(int id, UpdateProductDto updateCategoryDto);
        Task<ServiceResponse<bool>> DeleteProductAsync(int id);
    }
}
