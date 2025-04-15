using Domain.DTOs.TaskAssigmentDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface ITaskAssigmentService
{
    Task<Response<GetTaskAssignmentDTO>> AddTaskAssignment(CreateTaskAssignmentDTO createTaskAssignment);
    Task<Response<GetTaskAssignmentDTO>> UpdateTaskAssignment(int TaskAssignmentId, UpdateTaskAssignmentDTO updateTaskAssignment);
    Task<Response<string>> DeleteTaskAssignment(int TaskAssignmentId);
    Task<Response<GetTaskAssignmentDTO>> GetTaskAssignment(int TaskAssignmentId);
    Task<Response<List<GetTaskAssignmentDTO>>> GetAssignmentsByUser(int UserId);
    Task<Response<List<GetTaskAssignmentDTO>>> GetAssignmentsByTask(int TaskId);
}