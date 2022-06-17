using AutoMapper;
using FreeCourse.Services.Order.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCourse.Services.Order.Application.Mapping
{
    public class CustomMapping:Profile
    {
        public CustomMapping() 
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.Address, AddressDto>().ReverseMap();
           // CreateMap<Domain.OrderAggregate.Order, OrderDto>();


        }


    }
}
