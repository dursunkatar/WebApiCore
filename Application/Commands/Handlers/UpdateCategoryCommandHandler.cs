using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ServiceResponse<CategoryDto>>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ServiceResponse<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.UpdateCategoryAsync(request.Id, new UpdateCategoryDto
            {
                Id = request.Id,
                Name = request.Name
            });
        }
    }
}
