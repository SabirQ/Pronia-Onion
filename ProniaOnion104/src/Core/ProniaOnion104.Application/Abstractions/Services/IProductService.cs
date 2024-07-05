using ProniaOnion104.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductItemDto>> GetAllPaginated(int page, int take);
        Task<ProductGetDto> GetByIdAsync(int id);
        Task CreateAsync(ProductCreateDto dto);
        Task UpdateAsync(int id, ProductUpdateDto dto);
    }
}
