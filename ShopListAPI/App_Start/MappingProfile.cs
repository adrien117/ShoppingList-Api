using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ShopListAPI.Dtos;
using ShopListAPI.Models;

namespace ShopListAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           //okay.. lets do things the right way...
           Mapper.Initialize(config => config.CreateMap<ShopItem,ShopItemDto>());
        }
    }
}