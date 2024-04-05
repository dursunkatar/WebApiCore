using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResponse<ProductDto>>
    {
        private readonly IProductService _productService;

        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ServiceResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProductAsync(request.Id, new UpdateProductDto
            {
                Name = request.Name,
                Price = request.Price,
            });
        }
    }
}
