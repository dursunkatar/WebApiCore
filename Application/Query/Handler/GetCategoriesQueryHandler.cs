using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.Handler
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, ServiceResponse<IEnumerable<CategoryDto>>>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoriesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<ServiceResponse<IEnumerable<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetAllCategorysAsync();
        }
    }
}
