using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnAutoMapper.Models
{
    public class GenericSource<T>
    {
        public T Value { get; set; }
    }
}