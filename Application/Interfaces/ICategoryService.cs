using Application.DTOs;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse<CategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<ServiceResponse<IEnumerable<CategoryDto>>> GetAllCategorysAsync();
        Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(int id);
        Task<ServiceResponse<CategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
        Task<ServiceResponse<bool>> DeleteCategoryAsync(int id);
    }
}
