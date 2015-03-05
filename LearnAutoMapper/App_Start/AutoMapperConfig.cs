using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using LearnAutoMapper.Models;



namespace LearnAutoMapper.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Register the mappings for the application
            AutoMapper.Mapper.CreateMap<Order, OrderDto>();
        }




    }
}