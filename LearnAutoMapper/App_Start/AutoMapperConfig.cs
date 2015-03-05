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

            // Regular Mapping where Src and Dest names match
            AutoMapper.Mapper.CreateMap<Order, OrderDto>();

            // Mapping where Src and Dest names do not match
            Mapper.CreateMap<CalendarEvent, CalendarEventForm>()
                .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Date.Date))
                .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.Date.Hour))
                .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.Date.Minute));



        }




    }
}