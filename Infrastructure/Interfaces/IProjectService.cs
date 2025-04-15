using Domain.DTOs.ProjectDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IProjectService
{
    Task<Response<GetProjectDTO>> AddProject(CreateProjectDTO createProject);
    Task<Response<GetProjectDTO>> UpdateProject(int ProjectId, UpdateProjectDTO updateProject);
    Task<Response<string>> DeleteProject(int ProjectId);
    Task<Response<GetProjectDTO>> GetProject(int ProjectId);
    Task<Response<GetProjectDTO>> GetProjectWithMostTasks();
}