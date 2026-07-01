using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using praktika1.DTOs;
using praktika1.Models;

public interface IFilmService
{
    public Task<ServiceResult<PaginatedFilmsDTO>> GetPaginatedFilmsAsync(FilmQueryDTO filters, int pageSize);
    public Task<ServiceResult<List<FilmOptionDTO>>> GetOptionsAsync();
    public Task<ServiceResult<Film>> GetByIdAsync(int id);
    public Task<ServiceResult<int>> CreateAsync(CreateFilmDTO dto);
    public Task<ServiceResult> UpdateAsync(int id, CreateFilmDTO dto);
    public Task<ServiceResult> DeleteAsync(int id);
}

