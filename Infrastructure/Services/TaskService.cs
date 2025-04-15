using System.Net;
using Domain.DTOs.TaskDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TaskService(DataContext context) : ITaskService
{
    public async Task<Response<GetTaskDTO>> AddTask(CreateTaskDTO createTask)
    {
        var task = new Tasks
        {
            Title = createTask.Title,
            Description = createTask.Description,
            DueDate = createTask.DueDate,
            ProjectId = createTask.ProjectId,
            UserId = createTask.UserId
        };

        await context.Tasks.AddAsync(task);
        var result = await context.SaveChangesAsync();

        var getTaskDto = new GetTaskDTO
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            ProjectId = task.ProjectId,
            UserId = task.UserId
        };

        return result == 0
            ? new Response<GetTaskDTO>(HttpStatusCode.InternalServerError, "Task not added")
            : new Response<GetTaskDTO>(getTaskDto);
    }

    public async Task<Response<GetTaskDTO>> UpdateTask(int TaskId, UpdateTaskDTO updateTask)
    {
        var info = await context.Tasks.FindAsync(TaskId);
        if (info == null)
        {
            return new Response<GetTaskDTO>(HttpStatusCode.NotFound, "Task not found");   
        }

        info.Title = updateTask.Title;
        info.Description = updateTask.Description;
        info.DueDate = updateTask.DueDate;
        info.ProjectId = updateTask.ProjectId;
        info.UserId = updateTask.UserId;

        var result = await context.SaveChangesAsync();

        var getTaskDto = new GetTaskDTO
        {
            Id = info.Id,
            Title = info.Title,
            Description = info.Description,
            DueDate = info.DueDate,
            ProjectId = info.ProjectId,
            UserId = info.UserId
        };

        return result == 0
            ? new Response<GetTaskDTO>(HttpStatusCode.InternalServerError, "Task not updated")
            : new Response<GetTaskDTO>(getTaskDto);
    }

    public async Task<Response<string>> DeleteTask(int TaskId)
    {
        var info = await context.Tasks.FindAsync(TaskId);
        if (info == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Task not found");   
        }

        context.Tasks.Remove(info);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong")
            : new Response<string>("Task deleted successfully");
    }

    public async Task<Response<GetTaskDTO>> GetTask(int TaskId)
    {
        var task = await context.Tasks
            .Where(t => t.Id == TaskId)
            .Select(t => new GetTaskDTO
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                ProjectId = t.ProjectId,
                UserId = t.UserId
            })
            .FirstOrDefaultAsync();

        return task == null
            ? new Response<GetTaskDTO>(HttpStatusCode.NotFound, "Task not found")
            : new Response<GetTaskDTO>(task);
    }

    public async Task<Response<List<GetTaskDTO>>> GetTasksByProject(int ProjectId)
    {
        var tasks = await context.Tasks
            .Where(t => t.ProjectId == ProjectId)
            .Select(t => new GetTaskDTO
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                ProjectId = t.ProjectId,
                UserId = t.UserId
            })
            .ToListAsync();

        return tasks.Count == 0
            ? new Response<List<GetTaskDTO>>(HttpStatusCode.NotFound, "Tasks by project not found")
            : new Response<List<GetTaskDTO>>(tasks);
    }

    public async Task<Response<List<GetTaskDTO>>> GetTasksByUser(int UserId)
    {
        var tasks = await context.Tasks
            .Where(t => t.UserId == UserId)
            .Select(t => new GetTaskDTO
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                ProjectId = t.ProjectId,
                UserId = t.UserId
            })
            .ToListAsync();

        return tasks.Count == 0
            ? new Response<List<GetTaskDTO>>(HttpStatusCode.NotFound, "Tasks by user not found")
            : new Response<List<GetTaskDTO>>(tasks);
    }

    public async Task<Response<List<GetTaskDTO>>> GetTasksDueSoon(int Days)
    {
        var date = DateTime.Now.AddDays(Days);

        var tasks = await context.Tasks
            .Where(t => t.DueDate >= DateTime.Now && t.DueDate <= date)
            .Select(t => new GetTaskDTO
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                ProjectId = t.ProjectId,
                UserId = t.UserId
            })
            .ToListAsync();

        return tasks.Count == 0
            ? new Response<List<GetTaskDTO>>(HttpStatusCode.NotFound, "No due tasks found")
            : new Response<List<GetTaskDTO>>(tasks);
    }

}