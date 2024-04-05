﻿using Application.DTOs;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query
{
    public class GetProductsByCategoryQuery : IRequest<ServiceResponse<IEnumerable<ProductsOfCategoryDto>>>
    {
        public int? CategoryId { get; set; }
    }
}
