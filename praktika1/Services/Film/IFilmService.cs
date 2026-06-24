using MainProjectOOPIII3.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using praktika1.DTOs;
using praktika1.Models;

public interface IFilmService
{
    public Task<ServiceResult<PaginatedFilmsDTO>> GetPaginatedFilmsAsync(int page, int pageSize);
    public Task<ServiceResult<Film>> GetFilmByIdAsync(int id);
    public Task<ServiceResult<int>> CreateFilmAsync(Film film, int[] izabraniReziseri);
    public Task<ServiceResult> UpdateFilmAsync(int id, Film film, int[] izabraniReziseri);
    public Task<ServiceResult> DeleteFilmAsync(int id);

    public Task<ServiceResult<SelectList>> GetZanroviSelectListAsync(int? selectedId = null);
    public Task<ServiceResult<MultiSelectList>> GetReziseriMultiSelectListAsync(int[]? selectedIds = null);
}

