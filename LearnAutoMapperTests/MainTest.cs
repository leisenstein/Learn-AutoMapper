using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnAutoMapper;
using LearnAutoMapper.Models;
using AutoMapper;
using System.Collections.Generic;
using LearnAutoMapper.Services;

namespace LearnAutoMapperTests
{
    [TestClass]
    public class MainTest
    {
        [TestMethod]
        public void Flattening_Test()
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
        public void Projection_Test()
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



            // ForMember( PARAMETER , OPERATION )
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


        [TestMethod]
        public void ListAndArray_Test()
        {
            /*
             * AutoMapper only requires configuration of element types, not of any array or list type that might be used.
            */

            Mapper.CreateMap<ListAndArraySource, ListAndArrayDestination>();

            var sources = new[]
            {
                new ListAndArraySource { Value = 5 },
                new ListAndArraySource { Value = 6 },
                new ListAndArraySource { Value = 7 }
            };

            IEnumerable<ListAndArrayDestination> ienumerableDest = Mapper.Map<ListAndArraySource[], IEnumerable<ListAndArrayDestination>>(sources);
            ICollection<ListAndArrayDestination> icollectionDest = Mapper.Map<ListAndArraySource[], ICollection<ListAndArrayDestination>>(sources);
            IList<ListAndArrayDestination> ilistDest = Mapper.Map<ListAndArraySource[], IList<ListAndArrayDestination>>(sources);
            List<ListAndArrayDestination> listDest = Mapper.Map<ListAndArraySource[], List<ListAndArrayDestination>>(sources);
            ListAndArrayDestination[] arrayDest = Mapper.Map<ListAndArraySource[], ListAndArrayDestination[]>(sources);


            Assert.AreEqual(sources.Length, icollectionDest.Count);
            Assert.AreEqual(sources.Length, ilistDest.Count);
            Assert.AreEqual(sources.Length, listDest.Count);
            Assert.AreEqual(sources.Length, arrayDest.Length);

        }


        [TestMethod]
        public void Nested_Mapping()
        {
            /* 
             *  Nested objects can be mapped if the names/types match
            */
            
            Mapper.CreateMap<OuterSource, OuterDest>();
            Mapper.CreateMap<InnerSource, InnerDest>();
            Mapper.AssertConfigurationIsValid();

            var source = new OuterSource
            {
                Value = 5,
                Inner = new InnerSource { OtherValue = 15 }
            };

            var dest = Mapper.Map<OuterSource, OuterDest>(source);

            Assert.AreEqual(dest.Value, 5);
            Assert.IsNotNull(dest.Inner);
            Assert.AreEqual(dest.Inner.OtherValue, 15);


        }


        [TestMethod]
        public void CustomTypeConverter_Test()
        {
            /*
             * Sometimes, you need to take complete control over the conversion of one type to another. 
             * This is typically when one type looks nothing like the other, a conversion function already exists, and you would like to go from a "looser" type 
             * to a stronger type, such as a source type of string to a destination type of Int32.
             */

            Mapper.CreateMap<string, int>().ConvertUsing(Convert.ToInt32);
            Mapper.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            Mapper.CreateMap<string, Type>().ConvertUsing<TypeTypeConverter>();
            Mapper.CreateMap<TypeConverterSource, TypeConverterDestination>();
            Mapper.AssertConfigurationIsValid();

            var source = new TypeConverterSource
            {
                Value1 = "5",
                Value2 = "01/01/2000",
                Value3 = "AutoMapperSamples.GlobalTypeConverters.GlobalTypeConverters+Destination"
            };

            TypeConverterDestination result = Mapper.Map<TypeConverterSource, TypeConverterDestination>(source);
            Assert.AreEqual(result.Value1, 5);
            Assert.AreEqual(result.Value2, Convert.ToDateTime("01/01/2000"));
        }


        [TestMethod]
        public void NullSubstitution_Test()
        {
            /*
             * Null substitution allows you to supply an alternate value for a destination member if the source value is null anywhere along the member chain.
            */

            // If Value is NULL, put in "Other Value".  Otherwise, use its Value
            Mapper.CreateMap<NullSubSource, NullSubDest>().ForMember(dest => dest.Value, opt => opt.NullSubstitute("Other Value"));

            // Test for "Other Value"
            var source = new NullSubSource { Value = null };
            var destination = Mapper.Map<NullSubSource, NullSubDest>(source);
            Assert.AreEqual(destination.Value, "Other Value");


            // Test for Normal behavior
            source.Value = "Not null";
            destination = Mapper.Map<NullSubSource, NullSubDest>(source);
            Assert.AreEqual(destination.Value, "Not null");

        }



        [TestMethod]
        public void GenericsMapping_Test()
        {
            /*
             * AutoMapper can support an open generic type map. 
            */

            Mapper.CreateMap(typeof(GenericSource<>), typeof(GenericDestination<>));

            var source = new GenericSource<int> { Value = 10 };
            var dest = Mapper.Map<GenericSource<int>, GenericDestination<int>>(source);

            Assert.AreEqual(dest.Value, 10);
        }
        

        [TestMethod]
        public void Destination_Map_Does_Not_Exist_Test()
        {
            /*
             * This handles the case where the Destination target has a Property that is not available in the Source. 
             * Just Ignore it.
            */

            // ForMember( PARAMETER , OPERATION )
            Mapper.CreateMap<SourceWithOutProperty, DestinationWithProperty>().ForMember(m => m.Isbn, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();

            var src = new SourceWithOutProperty { Title = "Cujo", Author = "Stephen King" };
            var dto = Mapper.Map<SourceWithOutProperty, DestinationWithProperty>(src);

            Assert.AreEqual(src.Author, dto.Author);
            Assert.AreEqual(src.Title, dto.Title);
            Assert.AreEqual(default(string), dto.Isbn);
        }
    }
}
