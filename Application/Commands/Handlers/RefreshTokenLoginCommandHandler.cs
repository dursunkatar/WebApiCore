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
    public class RefreshTokenLoginCommandHandler :   IRequestHandler<RefreshTokenLoginCommand, ServiceResponse<RefreshTokenResponseDto>>
    {
        private readonly IUserService _userService;

        public RefreshTokenLoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ServiceResponse<RefreshTokenResponseDto>> Handle(RefreshTokenLoginCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ValidateUserRefreshTokenAsync(request);

        }
    }
}
