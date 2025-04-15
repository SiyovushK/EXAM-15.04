using Domain.DTOs.TaskDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface ITaskService
{
    Task<Response<GetTaskDTO>> AddTask(CreateTaskDTO createTask);
    Task<Response<GetTaskDTO>> UpdateTask(int TaskId, UpdateTaskDTO updateTask);
    Task<Response<string>> DeleteTask(int TaskId);
    Task<Response<GetTaskDTO>> GetTask(int TaskId);
    Task<Response<List<GetTaskDTO>>> GetTasksByProject(int ProjectId);
    Task<Response<List<GetTaskDTO>>> GetTasksByUser(int UserId);
    Task<Response<List<GetTaskDTO>>> GetTasksDueSoon(int Days);
}