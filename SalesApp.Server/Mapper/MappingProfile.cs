using AutoMapper;
using SalesApp.Server.DTOs;
using SalesApp.Server.Models;

namespace SalesApp.Server.Mapper;
public class MappingProfile : Profile{
    public MappingProfile() {
        CreateMap<Product, CreateProductDTO>().ReverseMap();
        CreateMap<Product, UpdateProductDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name));
    }
}

