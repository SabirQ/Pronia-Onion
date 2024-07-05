using ProniaOnion104.Application.Abstractions.Repositories;
using ProniaOnion104.Domain.Entities;
using ProniaOnion104.Persistence.Contexts;


namespace ProniaOnion104.Persistence.Implementations.Repositories
{
    internal class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context)
        {

        }
    }
}
