
namespace ProniaOnion104.Application.DTOs.Products;

    public record ProductUpdateDto (string Name, decimal Price, string SKU, string? Description,int CategoryId, ICollection<int> ColorIds);


