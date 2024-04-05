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

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ServiceResponse<CategoryDto>>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ServiceResponse<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.CreateCategoryAsync(new CreateCategoryDto { Name = request.Name });
        }
    }

}
