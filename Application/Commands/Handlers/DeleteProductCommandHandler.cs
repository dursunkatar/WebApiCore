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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ServiceResponse<bool>>
    {
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteProductAsync(request.Id);
        }
    }
}
