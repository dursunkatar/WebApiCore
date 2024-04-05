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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<CategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetListAsync(null);
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return new ServiceResponse<IEnumerable<CategoryDto>> { Data = categoryDtos };
        }

        public async Task<ServiceResponse<bool>> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(p => p.Id == id);
            if (category == null)
                return new ServiceResponse<bool> { Success = false, Message = "Kategori bulunamadı" };

            bool isUseCategory = await _categoryRepository.Query()
                .Include(p => p.Products)
                .AnyAsync(p => p.Id == id && p.Products.Any());

            if (isUseCategory)
                return new ServiceResponse<bool> { Success = false, Message = "İçinde ürün bulunan kategoriyi silemezsiniz" };

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<CategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            bool hasCategory = await _categoryRepository.AnyAsync(p => p.Name == createCategoryDto.Name);
            if (hasCategory)
                return new ServiceResponse<CategoryDto> { Success = false, Message = "Bu kategori sistemde kayıtlı" };

            var category = _mapper.Map<Category>(createCategoryDto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            var newCategoryDto = _mapper.Map<CategoryDto>(category);
            return new ServiceResponse<CategoryDto> { Data = newCategoryDto };
        }

        public async Task<ServiceResponse<IEnumerable<CategoryDto>>> GetAllCategorysAsync()
        {
            var categories = await _categoryRepository.GetListAsync(null);
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return new ServiceResponse<IEnumerable<CategoryDto>> { Data = categoryDtos };
        }

        public async Task<ServiceResponse<CategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryRepository.GetAsync(p => p.Id == id);
            if (category == null)
                return new ServiceResponse<CategoryDto> { Success = false, Message = "Kategori bulunamadı" };

            bool hasCategory = await _categoryRepository.AnyAsync(p => p.Id != id && p.Name == updateCategoryDto.Name);
            if (hasCategory)
                return new ServiceResponse<CategoryDto> { Success = false, Message = "Bu kategori sistemde kayıtlı" };

            category.Name = updateCategoryDto.Name;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
            var updatedCategoryDto = _mapper.Map<CategoryDto>(category);
            return new ServiceResponse<CategoryDto> { Data = updatedCategoryDto };
        }

        public async Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(p => p.Id == id);
            if (category == null)
                return new ServiceResponse<CategoryDto> { Success = false, Message = "Kategori bulunamadı" };

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return new ServiceResponse<CategoryDto>
            {
                Data = categoryDto
            };
        }
    }
}
