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
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, ServiceResponse<IEnumerable<ProductsOfCategoryDto>>>
    {
        private readonly IProductService _productService;

        public GetProductsByCategoryQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ServiceResponse<IEnumerable<ProductsOfCategoryDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetAllProductsAsync(request.CategoryId);
        }
    }
}
