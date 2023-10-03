using AutoMapper;
using DemoExam.Blazor.Shared;
using DemoExam.Domain.Models;

namespace DemoExam.Blazor.Server.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(x => x.ProductPhoto, 
                j => j.MapFrom(h => h.ProductPhoto == null 
                    ? null 
                    : Convert.ToBase64String(h.ProductPhoto)));
    }
}