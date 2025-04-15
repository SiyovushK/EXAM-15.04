using System.Net;
using Domain.DTOs.TaskAssigmentDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TaskAssigmentService(DataContext context) : ITaskAssigmentService
{
    public async Task<Response<GetTaskAssignmentDTO>> AddTaskAssignment(CreateTaskAssignmentDTO createTaskAssignment)
    {
        var TaskAssignment = new TaskAssignment
        {
            TaskId = createTaskAssignment.TaskId,
            UserId = createTaskAssignment.UserId
        };

        await context.TaskAssignments.AddAsync(TaskAssignment);
        var result = await context.SaveChangesAsync();

        var getTaskAssignmentDto = new GetTaskAssignmentDTO
        {
            Id = TaskAssignment.Id,
            TaskId = TaskAssignment.TaskId,
            UserId = TaskAssignment.UserId,
            AssignedDate = TaskAssignment.AssignedDate
        };

        return result == 0
            ? new Response<GetTaskAssignmentDTO>(HttpStatusCode.InternalServerError, "Task Assignment not added")
            : new Response<GetTaskAssignmentDTO>(getTaskAssignmentDto);
    }

    public async Task<Response<GetTaskAssignmentDTO>> UpdateTaskAssignment(int TaskAssignmentId, UpdateTaskAssignmentDTO updateTaskAssignment)
    {
        var info = await context.TaskAssignments.FindAsync(TaskAssignmentId);
        if (info == null)
        {
            return new Response<GetTaskAssignmentDTO>(HttpStatusCode.NotFound, "TaskAssignment not found");   
        }

        info.TaskId = updateTaskAssignment.TaskId;
        info.UserId = updateTaskAssignment.UserId;
        info.AssignedDate = updateTaskAssignment.AssignedDate;

        var result = await context.SaveChangesAsync();

        var getTaskAssignmentDto = new GetTaskAssignmentDTO
        {
            Id = info.Id,
            TaskId = info.TaskId,
            UserId = info.UserId,
            AssignedDate = info.AssignedDate
        };

        return result == 0
            ? new Response<GetTaskAssignmentDTO>(HttpStatusCode.InternalServerError, "Task Assignment not updated")
            : new Response<GetTaskAssignmentDTO>(getTaskAssignmentDto);
    }

    public async Task<Response<string>> DeleteTaskAssignment(int TaskAssignmentId)
    {
        var info = await context.TaskAssignments.FindAsync(TaskAssignmentId);
        if (info == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Task Assignment not found");   
        }

        context.TaskAssignments.Remove(info);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong")
            : new Response<string>("Task Assignment deleted successfully");
    }

    public async Task<Response<GetTaskAssignmentDTO>> GetTaskAssignment(int TaskAssignmentId)
    {
        var TaskAssignment = await context.TaskAssignments
            .Where(ta => ta.Id == TaskAssignmentId)
            .Select(ta => new GetTaskAssignmentDTO
            {
                Id = ta.Id,
                TaskId = ta.TaskId,
                UserId = ta.UserId,
                AssignedDate = ta.AssignedDate
            })
            .FirstOrDefaultAsync();

        return TaskAssignment == null
            ? new Response<GetTaskAssignmentDTO>(HttpStatusCode.NotFound, "Task Assignment not found")
            : new Response<GetTaskAssignmentDTO>(TaskAssignment);
    }

    public async Task<Response<List<GetTaskAssignmentDTO>>> GetAssignmentsByUser(int UserId)
    {
        var assignments = await context.TaskAssignments
            .Where(ta => ta.UserId == UserId)
            .Select(ta => new GetTaskAssignmentDTO
            {
                Id = ta.Id,
                TaskId = ta.TaskId,
                UserId = ta.UserId,
                AssignedDate = ta.AssignedDate
            })
            .ToListAsync();

        return assignments.Count == 0
            ? new Response<List<GetTaskAssignmentDTO>>(HttpStatusCode.NotFound, "Assignments by user not found")
            : new Response<List<GetTaskAssignmentDTO>>(assignments);
    }

    public async Task<Response<List<GetTaskAssignmentDTO>>> GetAssignmentsByTask(int TaskId)
    {
        var assignments = await context.TaskAssignments
            .Where(ta => ta.TaskId == TaskId)
            .Select(ta => new GetTaskAssignmentDTO
            {
                Id = ta.Id,
                TaskId = ta.TaskId,
                UserId = ta.UserId,
                AssignedDate = ta.AssignedDate
            })
            .ToListAsync();

        return assignments.Count == 0
            ? new Response<List<GetTaskAssignmentDTO>>(HttpStatusCode.NotFound, "Assignments by task not found")
            : new Response<List<GetTaskAssignmentDTO>>(assignments);
    }

}