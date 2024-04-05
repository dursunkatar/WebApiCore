using Application.DTOs;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateCategoryCommand : IRequest<ServiceResponse<CategoryDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
