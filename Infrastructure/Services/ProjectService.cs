using System.Net;
using Domain.DTOs.ProjectDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProjectService(DataContext context) : IProjectService
{
    public async Task<Response<GetProjectDTO>> AddProject(CreateProjectDTO createProject)
    {
        var project = new Project
        {
            Name = createProject.Name,
            Description = createProject.Description,
            StartDate = createProject.StartDate,
            EndDate = createProject.EndDate
        };

        await context.Projects.AddAsync(project);
        var result = await context.SaveChangesAsync();

        var getProjectDto = new GetProjectDTO
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate
        };

        return result == 0
            ? new Response<GetProjectDTO>(HttpStatusCode.InternalServerError, "Project not added")
            : new Response<GetProjectDTO>(getProjectDto);
    }

    public async Task<Response<GetProjectDTO>> UpdateProject(int ProjectId, UpdateProjectDTO updateProject)
    {
        var info = await context.Projects.FindAsync(ProjectId);
        if (info == null)
        {
            return new Response<GetProjectDTO>(HttpStatusCode.NotFound, "Project not found");   
        }

        info.Name = updateProject.Name;
        info.Description = updateProject.Description;
        info.StartDate = updateProject.StartDate;
        info.EndDate = updateProject.EndDate;

        var result = await context.SaveChangesAsync();

        var getProjectDto = new GetProjectDTO
        {
            Id = info.Id,
            Name = info.Name,
            Description = info.Description,
            StartDate = info.StartDate,
            EndDate = info.EndDate
        };

        return result == 0
            ? new Response<GetProjectDTO>(HttpStatusCode.InternalServerError, "Project not updated")
            : new Response<GetProjectDTO>(getProjectDto);
    }

    public async Task<Response<string>> DeleteProject(int ProjectId)
    {
        var info = await context.Projects.FindAsync(ProjectId);
        if (info == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Project not found");   
        }

        context.Projects.Remove(info);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong")
            : new Response<string>("Project deleted successfully");
    }

    public async Task<Response<GetProjectDTO>> GetProject(int ProjectId)
    {
        var project = await context.Projects
            .Where(p => p.Id == ProjectId)
            .Select(p => new GetProjectDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate
            })
            .FirstOrDefaultAsync();

        return project == null
            ? new Response<GetProjectDTO>(HttpStatusCode.NotFound, "Project not found")
            : new Response<GetProjectDTO>(project);
    }

    public async Task<Response<GetProjectDTO>> GetProjectWithMostTasks()
    {
        var project = await context.Tasks
            .GroupBy(t => t.Project)
            .OrderByDescending(g => g.Count())
            .FirstOrDefaultAsync();
        
        if (project == null)
        {
            return new Response<GetProjectDTO>(HttpStatusCode.NotFound, "Project with most tasks are not found");
        }

        var projectDTO = new GetProjectDTO
        {
            Id = project.Key.Id,
            Name = project.Key.Name,
            Description = project.Key.Description,
            StartDate = project.Key.StartDate,
            EndDate = project.Key.EndDate
        };

        return new Response<GetProjectDTO>(projectDTO);
    }
}