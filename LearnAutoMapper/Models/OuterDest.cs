using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnAutoMapper.Models
{
    public class OuterDest
    {
        public int Value { get; set; }
        public InnerDest Inner { get; set; }
    }
}