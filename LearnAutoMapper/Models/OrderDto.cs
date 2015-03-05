using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnAutoMapper.Models
{
    public class OrderDto
    {
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
    }
}