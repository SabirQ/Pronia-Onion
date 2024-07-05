using AutoMapper;
using ProniaOnion104.Application.DTOs.Products;
using ProniaOnion104.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Application.MappingProfiles
{
    internal class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductItemDto>().ReverseMap();
            CreateMap<Product, ProductGetDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
        }
    }
}
