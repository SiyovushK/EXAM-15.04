using Domain.DTOs.ProjectDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    
    [HttpPost]
    public async Task<Response<GetProjectDTO>> AddProject(CreateProjectDTO createProject)
    {
        return await projectService.AddProject(createProject);
    }
    
    [HttpPut]
    public async Task<Response<GetProjectDTO>> UpdateProject(int ProjectId, UpdateProjectDTO updateProject)
    {
        return await projectService.UpdateProject(ProjectId, updateProject);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteProject(int ProjectId)
    {
        return await projectService.DeleteProject(ProjectId);
    }
    
    [HttpGet("projectId")]
    public async Task<Response<GetProjectDTO>> GetProject(int ProjectId)
    {
        return await projectService.GetProject(ProjectId);
    }
    
    [HttpGet("mostTasksProject")]
    public async Task<Response<GetProjectDTO>> GetProjectWithMostTasks()
    {
        return await projectService.GetProjectWithMostTasks();
    }
}