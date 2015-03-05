using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnAutoMapper.Models;

namespace LearnAutoMapperTest
{
    [TestClass]
    public class Main
    {
        [TestMethod]
        public void TestAutoMapper1()
        {

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

            //// Configure AutoMapper

            //Mapper.CreateMap<Order, OrderDto>();

            //// Perform mapping

            OrderDto dto = Mapper.Map<Order, OrderDto>(order);

            dto.CustomerName.ShouldEqual("George Costanza");
            dto.Total.ShouldEqual(74.85m);




        }
    }
}
