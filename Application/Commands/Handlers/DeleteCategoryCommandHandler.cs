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
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ServiceResponse<bool>>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.DeleteCategoryAsync(request.Id);
        }
    }
}
