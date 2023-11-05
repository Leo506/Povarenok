﻿using AutoMapper;
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
                    : Convert.ToBase64String(h.ProductPhoto)))
            .ForMember(x => x.ProductArticleNumber, j => j.MapFrom(h => h.ArticleNumber));

        CreateMap<Order, OrderShortDto>()
            .ForMember(x => x.PickupPoint,
                j => j.MapFrom(h => h.OrderPickupPointNavigation.AddressString))
            .ForMember(x => x.Status,
                j => j.MapFrom(h => h.OrderStatus))
            .ForMember(x => x.DeliveryDate,
                j => j.MapFrom(h => DateOnly.FromDateTime(h.OrderDeliveryDate)));

        CreateMap<Order, OrderDto>()
            .ForMember(x => x.PickupPoint,
                j => j.MapFrom(h => h.OrderPickupPointNavigation.AddressString))
            .ForMember(x => x.Status,
                j => j.MapFrom(h => h.OrderStatus))
            .ForMember(x => x.DeliveryDate,
                j => j.MapFrom(h => DateOnly.FromDateTime(h.OrderDeliveryDate)))
            .ForMember(x => x.OrderDate,
                j => j.MapFrom(h => DateOnly.FromDateTime(h.OrderDate)))
            .ForMember(x => x.OrderItems,
                j => j.MapFrom(x => x.OrderItems));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(x => x.Article,
                j => j.MapFrom(h => h.Product.ArticleNumber))
            .ForMember(x => x.Title,
                j => j.MapFrom(h => h.Product.ProductName))
            .ForMember(x => x.TotalCost,
                j => j.MapFrom(h => h.Product.ProductCost * h.Amount));

        CreateMap<PickupPoint, PickupPointDto>()
            .ForMember(x => x.Address,
                j => j.MapFrom(h => h.AddressString));
    }
}