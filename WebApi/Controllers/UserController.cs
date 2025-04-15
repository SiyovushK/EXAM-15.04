using Domain.DTOs.UserDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetUserDTO>> AddUser(CreateUserDTO createUser)
    {
        return await userService.AddUser(createUser);
    }
    
    [HttpPut]
    public async Task<Response<GetUserDTO>> UpdateUser(int UserId, UpdateUserDTO updateUser)
    {
        return await userService.UpdateUser(UserId, updateUser);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteUser(int UserId)
    {
        return await userService.DeleteUser(UserId);
    }
    
    [HttpGet("userId")]
    public async Task<Response<GetUserDTO>> GetUser(int UserId)
    {
        return await userService.GetUser(UserId);
    }
    
    [HttpGet("userWithMostTasks")]
    public async Task<Response<GetUserDTO>> GetUserWithMostTasks()
    {
        return await userService.GetUserWithMostTasks();
    }
}