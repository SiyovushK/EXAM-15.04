using Domain.DTOs.TaskAssigmentDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskAssigmentController(ITaskAssigmentService taskAssigment) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetTaskAssignmentDTO>> AddTaskAssignment(CreateTaskAssignmentDTO createTaskAssignment)
    {
        return await taskAssigment.AddTaskAssignment(createTaskAssignment);
    }
    
    [HttpPut]
    public async Task<Response<GetTaskAssignmentDTO>> UpdateTaskAssignment(int TaskAssignmentId, UpdateTaskAssignmentDTO updateTaskAssignment)
    {
        return await taskAssigment.UpdateTaskAssignment(TaskAssignmentId, updateTaskAssignment);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteTaskAssignment(int TaskAssignmentId)
    {
        return await taskAssigment.DeleteTaskAssignment(TaskAssignmentId);
    }
    
    [HttpGet("taskAssignmentId")]
    public async Task<Response<GetTaskAssignmentDTO>> GetTaskAssignment(int TaskAssignmentId)
    {
        return await taskAssigment.GetTaskAssignment(TaskAssignmentId);
    }
    
    [HttpGet("userId")]
    public async Task<Response<List<GetTaskAssignmentDTO>>> GetAssignmentsByUser(int UserId)
    {
        return await taskAssigment.GetAssignmentsByUser(UserId);
    }
    
    [HttpGet("taskId")]
    public async Task<Response<List<GetTaskAssignmentDTO>>> GetAssignmentsByTask(int TaskId)
    {
        return await taskAssigment.GetAssignmentsByTask(TaskId);
    }
}