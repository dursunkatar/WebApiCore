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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ServiceResponse<UserDto>>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ServiceResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.CreateUserAsync(new CreateUserDto
            {
                Email = request.Email,
                Password = request.Password,
                Username = request.UserName
            });
        }
    }
}
