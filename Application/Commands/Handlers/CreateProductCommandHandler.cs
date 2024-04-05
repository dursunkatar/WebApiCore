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
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<ProductDto>>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<ServiceResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
           return await _productService.CreateProductAsync(new CreateProductDto
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Price = request.Price,
            });
        }
    }
}
