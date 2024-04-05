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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ServiceResponse<bool>>
    {
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.DeleteUserAsync(request.UserId);
        }
    }

}
