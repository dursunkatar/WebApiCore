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
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ServiceResponse<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ServiceResponse<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
           return await _userService.GetUserByIdAsync(request.UserId);
        }
    }

}
