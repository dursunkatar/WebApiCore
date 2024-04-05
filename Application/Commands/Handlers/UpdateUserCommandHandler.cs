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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ServiceResponse<UserDto>>
    {
        private readonly IUserService _userService;
        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ServiceResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUserAsync(request.Id, new UpdateUserDto
            {
                Email = request.Email,
                Password = request.Password,
                UserName = request.UserName
            });
        }
    }

}
