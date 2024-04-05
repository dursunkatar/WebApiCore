using Application.DTOs;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query
{
    public class GetUsersQuery : IRequest<ServiceResponse<IEnumerable<UserDto>>>
    {
    }

}
