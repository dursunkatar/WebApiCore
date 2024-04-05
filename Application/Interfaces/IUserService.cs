using Application.Commands;
using Application.DTOs;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<UserDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<ServiceResponse<UserDto>> GetUserByIdAsync(int id);
        Task<ServiceResponse<UserDto>> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task<ServiceResponse<bool>> DeleteUserAsync(int id);
        Task<ServiceResponse<TokenResponseDto>> ValidateUserAsync(UserLoginCommand loginCommand);
        Task<ServiceResponse<RefreshTokenResponseDto>> ValidateUserRefreshTokenAsync(RefreshTokenLoginCommand refreshToken);
    }

}
