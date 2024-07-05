
using ProniaOnion104.Application.DTOs.Categories;

namespace ProniaOnion104.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryItemDto>> GetAllAsync(int page, int take);
        //Task<GetCategoryDto> GetAsync(int id);
        Task CreateAsync(CategoryCreateDto categoryDto);
        Task SoftDeleteAsync(int id);
    }
}
