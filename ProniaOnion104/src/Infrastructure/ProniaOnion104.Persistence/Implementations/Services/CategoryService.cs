using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion104.Application.Abstractions.Repositories;
using ProniaOnion104.Application.Abstractions.Services;
using ProniaOnion104.Application.DTOs.Categories;
using ProniaOnion104.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Persistence.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }



        public async Task<ICollection<CategoryItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllWhere(skip: (page - 1) * take, take: take, isTracking: false,ignoreQuery:true).ToListAsync();

            ICollection<CategoryItemDto> categoryDtos = _mapper.Map<ICollection<CategoryItemDto>>(categories);
           
            return categoryDtos;
        }
        public async Task CreateAsync(CategoryCreateDto categoryDto)
        {
            await _repository.AddAsync(_mapper.Map<Category>(categoryDto));
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");
            _repository.SoftDelete(category);
            await _repository.SaveChangesAsync();
            
        }
        //public async Task<GetCategoryDto> GetAsync(int id)
        //{
        //    Category category = await _repository.GetByIdAsync(id);

        //    if (category == null) throw new Exception("Not found");

        //    return new GetCategoryDto
        //    {
        //        Id = category.Id,
        //        Name = category.Name
        //    };
        //}


    }
}
