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
        public void Main1Test()
        {
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
    }
}
