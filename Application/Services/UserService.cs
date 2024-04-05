using Application.Commands;
using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshRepository _refreshRepository;
        private readonly PasswordHashHelper _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper, PasswordHashHelper passwordHasher, IRefreshRepository refreshRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _refreshRepository = refreshRepository;
        }

        public async Task<ServiceResponse<RefreshTokenResponseDto>> ValidateUserRefreshTokenAsync(RefreshTokenLoginCommand refreshToken)
        {
            var userrefreshToken = await _refreshRepository
                .Query()
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Token == refreshToken.Token);

            if (userrefreshToken is null)
                return new ServiceResponse<RefreshTokenResponseDto> { Success = false, Message = "Geçersiz token" };

            if (userrefreshToken.IsExpired)
                return new ServiceResponse<RefreshTokenResponseDto> { Success = false, Message = "Token süresi dolmuş" };

            return new ServiceResponse<RefreshTokenResponseDto>
            {
                Success = true,
                Data = new RefreshTokenResponseDto
                {
                    UserName = userrefreshToken.User.Username,
                    RefreshToken = userrefreshToken.Token
                }
            };
        }
        public async Task<ServiceResponse<TokenResponseDto>> ValidateUserAsync(UserLoginCommand loginCommand)
        {
            var user = await _userRepository.GetAsync(p => p.Username == loginCommand.Username);

            if (user is null)
                return new ServiceResponse<TokenResponseDto> { Success = false, Message = "Geçersiz kullanıcı adı" };

            bool verifyPassword = _passwordHasher.VerifyPassword(user.PasswordHash, loginCommand.Password);

            if (!verifyPassword)
                return new ServiceResponse<TokenResponseDto> { Success = false, Message = "Şifre yanlış" };

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
            };

            var userrefreshToken = await _refreshRepository.GetAsync(p => p.UserId == user.Id);
            if (userrefreshToken is not null)
            {
                _refreshRepository.Delete(userrefreshToken);
            }
            await _refreshRepository.AddAsync(refreshToken);
            await _refreshRepository.SaveChangesAsync();

            return new ServiceResponse<TokenResponseDto>
            {
                Success = true,
                Data = new TokenResponseDto
                {
                    UserName = user.Username,
                    RefreshToken = refreshToken.Token
                }
            };
        }

        private async Task<(bool, ServiceResponse<T>)> HasUserNameOrEmail<T>(int? id, string userName, string email)
        {
            bool hasUserName = await _userRepository.AnyAsync(p => (!id.HasValue || p.Id != id.Value) && p.Username == userName);

            if (hasUserName)
                return (false, new ServiceResponse<T> { Success = false, Message = "Bu kullanıcı adı sistemde kayıtlı" });

            bool hasEmail = await _userRepository.AnyAsync(p => (!id.HasValue || p.Id != id.Value) && p.Email == email);

            if (hasEmail)
                return (false, new ServiceResponse<T> { Success = false, Message = "Bu email adresi sistemde kayıtlı" });

            return (true, default);
        }
        public async Task<ServiceResponse<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var (ok, res) = await HasUserNameOrEmail<UserDto>(null, createUserDto.Username, createUserDto.Email);

            if (!ok) return res;

            var user = _mapper.Map<User>(createUserDto);
            user.PasswordHash = _passwordHasher.HashPassword(createUserDto.Password);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            var userDto = _mapper.Map<UserDto>(user);
            return new ServiceResponse<UserDto> { Data = userDto };
        }

        public async Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetListAsync(null);
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return new ServiceResponse<IEnumerable<UserDto>> { Data = userDtos };
        }

        public async Task<ServiceResponse<UserDto>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetAsync(p => p.Id == id);
            if (user == null)
            {
                return new ServiceResponse<UserDto> { Success = false, Message = "Kullanıcı bulunamadı" };
            }
            var userDto = _mapper.Map<UserDto>(user);
            return new ServiceResponse<UserDto> { Data = userDto };
        }

        public async Task<ServiceResponse<UserDto>> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetAsync(p => p.Id == id);
            if (user == null)
                return new ServiceResponse<UserDto> { Success = false, Message = "Kullanıcı bulunamadı" };

            var (ok, res) = await HasUserNameOrEmail<UserDto>(id, updateUserDto.UserName, updateUserDto.Email);

            if (!ok) return res;

            user.Username = updateUserDto.UserName;
            user.Email = updateUserDto.Email;

            if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(updateUserDto.Password);
            }
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            var userDto = _mapper.Map<UserDto>(user);
            return new ServiceResponse<UserDto> { Data = userDto };
        }

        public async Task<ServiceResponse<bool>> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetAsync(p => p.Id == id);
            if (user == null)
            {
                return new ServiceResponse<bool> { Success = false, Message = "Kullanıcı bulunamadı" };
            }
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }
    }
}
