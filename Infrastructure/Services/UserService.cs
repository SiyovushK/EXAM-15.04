using System.Net;
using Domain.DTOs.UserDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(DataContext context) : IUserService
{
    public async Task<Response<GetUserDTO>> AddUser(CreateUserDTO createUser)
    {
        var user = new User
        {
            Name = createUser.Name,
            Email = createUser.Email
        };

        await context.Users.AddAsync(user);
        var result = await context.SaveChangesAsync();

        var getUserDto = new GetUserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            RegistrationDate = user.RegistrationDate
        };

        return result == 0
            ? new Response<GetUserDTO>(HttpStatusCode.InternalServerError, "User not added")
            : new Response<GetUserDTO>(getUserDto);
    }

    public async Task<Response<GetUserDTO>> UpdateUser(int UserId, UpdateUserDTO updateUser)
    {
        var info = await context.Users.FindAsync(UserId);
        if (info == null)
        {
            return new Response<GetUserDTO>(HttpStatusCode.NotFound, "User not found");   
        }

        info.Name = updateUser.Name;
        info.Email = updateUser.Email;

        var result = await context.SaveChangesAsync();

        var getUserDto = new GetUserDTO
        {
            Id = info.Id,
            Name = info.Name,
            Email = info.Email,
            RegistrationDate = info.RegistrationDate
        };

        return result == 0
            ? new Response<GetUserDTO>(HttpStatusCode.InternalServerError, "User not updated")
            : new Response<GetUserDTO>(getUserDto);
    }

    public async Task<Response<string>> DeleteUser(int UserId)
    {
        var info = await context.Users.FindAsync(UserId);
        if (info == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "User not found");   
        }

        context.Users.Remove(info);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong")
            : new Response<string>("User deleted successfully");
    }

    public async Task<Response<GetUserDTO>> GetUser(int UserId)
    {
        var user = await context.Users
            .Where(u => u.Id == UserId)
            .Select(u => new GetUserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                RegistrationDate = u.RegistrationDate
            })
            .FirstOrDefaultAsync();

        return user == null
            ? new Response<GetUserDTO>(HttpStatusCode.NotFound, "User not found")
            : new Response<GetUserDTO>(user);
    }

    public async Task<Response<GetUserDTO>> GetUserWithMostTasks()
    {
        var user = await context.Tasks
            .GroupBy(t => t.User)
            .OrderByDescending(t => t.Count())
            .Select(t => new GetUserDTO
            {
                Id = t.Key.Id,
                Name = t.Key.Name,
                Email = t.Key.Email,
                RegistrationDate = t.Key.RegistrationDate
            })
            .FirstOrDefaultAsync();

        return user == null
            ? new Response<GetUserDTO>(HttpStatusCode.NotFound, "User with most tasks is not found")
            : new Response<GetUserDTO>(user);
    }
}