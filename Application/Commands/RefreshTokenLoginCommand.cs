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
    public class RefreshTokenLoginCommand : IRequest<ServiceResponse<RefreshTokenResponseDto>>
    {
        public string Token { get; set; }
    }
}
