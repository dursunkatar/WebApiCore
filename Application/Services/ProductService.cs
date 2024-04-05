using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryReposit;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository ProductRepository, IMapper mapper, ICategoryRepository categoryReposit)
        {
            _productRepository = ProductRepository;
            _mapper = mapper;
            _categoryReposit = categoryReposit;
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            var Product = await _productRepository.GetAsync(p => p.Id == id);
            if (Product == null)
                return new ServiceResponse<bool> { Success = false, Message = "Ürün bulunamadı" };

            _productRepository.Delete(Product);
            await _productRepository.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<ProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            bool hasProduct = await _productRepository.AnyAsync(p => p.Name == createProductDto.Name);
            if (hasProduct)
                return new ServiceResponse<ProductDto> { Success = false, Message = "Bu ürün sistemde kayıtlı" };

            bool hasCategory = await _categoryReposit.AnyAsync(p => p.Id == createProductDto.CategoryId);

            if (!hasCategory)
                return new ServiceResponse<ProductDto> { Success = false, Message = "Kategori bulunamadı" };


            var entity = new Product
            {
                CategoryId = createProductDto.CategoryId,
                Price = createProductDto.Price,
                Name = createProductDto.Name
            };
            await _productRepository.AddAsync(entity);
            await _productRepository.SaveChangesAsync();

            return new ServiceResponse<ProductDto>
            {
                Data = new ProductDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Price = entity.Price,
                    CategoryId = entity.CategoryId
                }
            };
        }

        public async Task<ServiceResponse<IEnumerable<ProductsOfCategoryDto>>> GetAllProductsAsync(int? categoryId)
        {
            var products = await _productRepository
                .Query()
                .Include(p => p.Category)
                .Where(p => !categoryId.HasValue || p.CategoryId == categoryId.Value)
                .ToListAsync();

            var productsOfCategories = products
                 .GroupBy(p => p.Category.Name)
                 .Select(
                      g => new ProductsOfCategoryDto
                      {
                          CategoryName = g.Key,
                          Products = g.Select(p => new ProductDto
                          {
                              CategoryId = p.CategoryId,
                              Name = p.Name,
                              Id = p.Id,
                              Price = p.Price
                          })
                      }
                 );

            return new ServiceResponse<IEnumerable<ProductsOfCategoryDto>> { Data = productsOfCategories };
        }

        public async Task<ServiceResponse<ProductDto>> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetAsync(p => p.Id == id);
            if (product == null)
                return new ServiceResponse<ProductDto> { Success = false, Message = "Ürün bulunamadı" };

            bool hasProduct = await _productRepository.AnyAsync(p => p.Id != id && p.Name == updateProductDto.Name);
            if (hasProduct)
                return new ServiceResponse<ProductDto> { Success = false, Message = "Bu ürün sistemde kayıtlı" };

            product.Price = updateProductDto.Price;
            product.Name = updateProductDto.Name;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            return new ServiceResponse<ProductDto>
            {
                Data = new ProductDto
                {
                    CategoryId= product.CategoryId,
                    Name= product.Name,
                    Id = product.Id,
                    Price = product.Price
                }
            };
        }

        public async Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int id)
        {
            var Product = await _productRepository.GetAsync(p => p.Id == id);
            if (Product == null)
                return new ServiceResponse<ProductDto> { Success = false, Message = "Ürün bulunamadı" };

            var ProductDto = _mapper.Map<ProductDto>(Product);
            return new ServiceResponse<ProductDto>
            {
                Data = ProductDto
            };
        }
    }
}
