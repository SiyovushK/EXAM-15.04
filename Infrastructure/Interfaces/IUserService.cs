using Domain.DTOs.UserDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<GetUserDTO>> AddUser(CreateUserDTO createUser);
    Task<Response<GetUserDTO>> UpdateUser(int UserId, UpdateUserDTO updateUser);
    Task<Response<string>> DeleteUser(int UserId);
    Task<Response<GetUserDTO>> GetUser(int UserId);
    Task<Response<GetUserDTO>> GetUserWithMostTasks();
}