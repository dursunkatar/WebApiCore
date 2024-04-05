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
    public class CreateUserCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
