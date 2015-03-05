using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnAutoMapper;
using LearnAutoMapper.Models;
using AutoMapper;

namespace LearnAutoMapperTests
{
    [TestClass]
    public class MainTest
    {
        [TestMethod]
        public void FlatteningTest()
        {
            /*
             * When you configure a source/destination type pair in AutoMapper, the configurator attempts to match properties and methods on the source type 
             * to properties on the destination type. 
             * If for any property on the destination type a property, method, or a method prefixed with "Get" does not exist on the source type, 
             * AutoMapper splits the destination member name into individual words (by PascalCase conventions).
             */

            // Register the mappings for the test.  AutoMapperConfig is not used here.
            AutoMapper.Mapper.CreateMap<Order, OrderDto>();

            var customer = new Customer
            {
                Name = "George Costanza"
            };
            var order = new Order
            {
                Customer = customer
            };
            var bosco = new Product
            {
                Name = "Bosco",
                Price = 4.99m
            };
            order.AddOrderLineItem(bosco, 15);

            OrderDto dto = Mapper.Map<Order, OrderDto>(order);

            Assert.AreEqual(dto.CustomerName, "George Costanza");
            Assert.AreEqual(dto.Total,74.85m);
        }


        [TestMethod]
        public void ProjectionTest()
        {
            /*
             * Projection transforms a source to a destination beyond flattening the object model. 
             * Without extra configuration, AutoMapper requires a flattened destination to match the source type's naming structure. 
             * When you want to project source values into a destination that does not exactly match the source structure, you must specify custom member mapping definitions.
            */

            var calendarEvent = new CalendarEvent
            {
                Date = new DateTime(2008, 12, 15, 20, 30, 0),
                Title = "Company Holiday Party"
            };



            // Configure AutoMapper
            Mapper.CreateMap<CalendarEvent, CalendarEventForm>()
                .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Date.Date))
                .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.Date.Hour))
                .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.Date.Minute));

            // Perform mapping
            CalendarEventForm form = Mapper.Map<CalendarEvent, CalendarEventForm>(calendarEvent);

            Assert.AreEqual(form.EventDate, new DateTime(2008, 12, 15));
            Assert.AreEqual(form.EventHour, 20);
            Assert.AreEqual(form.EventMinute, 30);
            Assert.AreEqual(form.Title, "Company Holiday Party");
        }
    }
}
