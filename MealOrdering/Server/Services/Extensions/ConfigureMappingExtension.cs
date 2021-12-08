using AutoMapper;
using MealOrdering.Server.Data.Models;
using MealOrdering.Shared.Dto;
using MealOrdering.Shared.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Extensions
{
    public static class  ConfigureMappingExtension
    {
        public static IServiceCollection ConfigureMapping(this IServiceCollection service)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            IMapper mapper = mappingConfig.CreateMapper();

            service.AddSingleton(mapper);

            return service;
        }
    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullDestinationValues = true;
            AllowNullCollections = true;

            CreateMap<Suppliers, SuppliersDto>()
                .ReverseMap();

            CreateMap<Users, UserDto>();

           CreateMap<UserDto, Users>().ForMember(x => x.Password, y => y.MapFrom(z => PasswordEncrypter.Encrypt(z.Password)));

            CreateMap<Orders, OrderDto>()
                .ForMember(x => x.SupplierName, y => y.MapFrom(z => z.Supplier.Name))
                .ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.CreatedUser.FirstName + " " + z.CreatedUser.LastName));

            CreateMap<OrderDto, Orders>();



            CreateMap<OrderItems, OrderItemsDto>()
                .ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.CreatedUser.FirstName + " " + z.CreatedUser.LastName))
                .ForMember(x => x.OrderName, y => y.MapFrom(z => z.Orders.Name ?? ""));

            CreateMap<OrderItemsDto, OrderItems>();
        }
    }
}

