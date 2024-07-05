using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion104.Application.Abstractions.Repositories;
using ProniaOnion104.Application.Abstractions.Services;
using ProniaOnion104.Application.DTOs.Products;
using ProniaOnion104.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Persistence.Implementations.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository repository,
            ICategoryRepository categoryRepository,
            IColorRepository colorRepository, 
            IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductItemDto>> GetAllPaginated(int page,int take)
        {
            IEnumerable<ProductItemDto> dtos = _mapper.Map<IEnumerable<ProductItemDto>>(await _repository.GetAllWhere(skip:(page-1)*take,take:take,isTracking:false).ToArrayAsync());
            return dtos;
        }
        public async Task<ProductGetDto> GetByIdAsync(int id)
        {
            Product product=await _repository.GetByIdAsync(id,includes:nameof(Product.Category));
            ProductGetDto dto=_mapper.Map<ProductGetDto>(product);
            return dto;
        }

        public async Task CreateAsync(ProductCreateDto dto)
        {
            if (await _repository.IsExistAsync(p => p.Name == dto.Name)) throw new Exception("Pro with name already exist");
            if (!await _categoryRepository.IsExistAsync(c=>c.Id==dto.CategoryId)) throw new Exception("dont");

            Product product=_mapper.Map<Product>(dto);
            product.ProductColors = new List<ProductColor>();
            foreach (var colorId in dto.ColorIds)
            {
                if (!await _colorRepository.IsExistAsync(c => c.Id == colorId)) throw new Exception("dont");
                product.ProductColors.Add(new ProductColor{ ColorId = colorId });
            }
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

        }

        public async Task UpdateAsync(int id,ProductUpdateDto dto)
        {
            Product existed=await _repository.GetByIdAsync(id,includes:nameof(Product.ProductColors));
            if (existed is null) throw new Exception("dont");
          
            if (dto.CategoryId != existed.CategoryId)
                if (!await _categoryRepository.IsExistAsync(c => c.Id == dto.CategoryId))
                    throw new Exception("dont");
   
            existed = _mapper.Map(dto,existed);
            existed.ProductColors = existed.ProductColors.Where(pc => dto.ColorIds.Any(colId => pc.ColorId == colId)).ToList();
            
            foreach (var cId in dto.ColorIds)
            {
                if (!await _colorRepository.IsExistAsync(c => c.Id == cId)) throw new Exception("dont");
                if (!existed.ProductColors.Any(pc=>pc.ColorId==cId))
                {
                    existed.ProductColors.Add(new ProductColor { ColorId = cId });
                }   
            }
            _repository.Update(existed);
            await _repository.SaveChangesAsync();


        }
    }
}
