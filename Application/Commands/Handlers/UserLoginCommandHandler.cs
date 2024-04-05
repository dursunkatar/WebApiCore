using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Handlers
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, ServiceResponse<TokenResponseDto>>
    {
        private readonly IUserService _userService;

        public UserLoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ServiceResponse<TokenResponseDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ValidateUserAsync(request);
            
        }
    }

}
