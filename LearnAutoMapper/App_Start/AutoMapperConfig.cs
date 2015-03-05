using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using LearnAutoMapper.Models;
using LearnAutoMapper.Services;


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



            Mapper.CreateMap<ListAndArraySource, ListAndArrayDestination>();


            Mapper.CreateMap<OuterSource, OuterDest>();
            Mapper.CreateMap<InnerSource, InnerDest>();

            Mapper.CreateMap<string, int>().ConvertUsing(Convert.ToInt32);
            Mapper.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            Mapper.CreateMap<string, Type>().ConvertUsing<TypeTypeConverter>();
            Mapper.CreateMap<TypeConverterSource, TypeConverterDestination>();

            Mapper.CreateMap<NullSubSource, NullSubDest>().ForMember(dest => dest.Value, opt => opt.NullSubstitute("Other Value"));

            Mapper.CreateMap(typeof(GenericSource<>), typeof(GenericDestination<>));

            Mapper.CreateMap<SourceWithOutProperty, DestinationWithProperty>().ForMember(m => m.Isbn, opt => opt.Ignore());


            Mapper.AssertConfigurationIsValid();
        }




    }
}