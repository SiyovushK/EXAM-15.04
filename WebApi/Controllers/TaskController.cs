using Domain.DTOs.TaskDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(ITaskService taskService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetTaskDTO>> AddTask(CreateTaskDTO createTask)
    {
        return await taskService.AddTask(createTask);
    }
    
    [HttpPut]
    public async Task<Response<GetTaskDTO>> UpdateTask(int TaskId, UpdateTaskDTO updateTask)
    {
        return await taskService.UpdateTask(TaskId, updateTask);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteTask(int TaskId)
    {
        return await taskService.DeleteTask(TaskId);
    }
    
    [HttpGet("taskId")]
    public async Task<Response<GetTaskDTO>> GetTask(int TaskId)
    {
        return await taskService.GetTask(TaskId);
    }
    
    [HttpGet("projectId")]
    public async Task<Response<List<GetTaskDTO>>> GetTasksByProject(int ProjectId)
    {
        return await taskService.GetTasksByProject(ProjectId);
    }
    
    [HttpGet("userId")]
    public async Task<Response<List<GetTaskDTO>>> GetTasksByUser(int UserId)
    {
        return await taskService.GetTasksByUser(UserId);
    }
    
    [HttpGet("dueTasks")]
    public async Task<Response<List<GetTaskDTO>>> GetTasksDueSoon(int Days)
    {
        return await taskService.GetTasksDueSoon(Days);
    }
}