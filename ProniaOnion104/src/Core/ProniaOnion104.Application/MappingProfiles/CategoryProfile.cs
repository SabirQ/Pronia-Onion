using AutoMapper;
using ProniaOnion104.Application.DTOs.Categories;
using ProniaOnion104.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Application.MappingProfiles
{
    internal class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryItemDto>().ReverseMap();
            //CreateMap<CategoryItemDto,Category>(); ehtiyac yoxdur artiq
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category,IncludeCategoryDto>().ReverseMap();

        }
    }
}
