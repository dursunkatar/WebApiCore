using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteUserCommand : IRequest<ServiceResponse<bool>>
    {
        public int UserId { get; set; }
    }
}
