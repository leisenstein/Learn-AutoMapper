using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnAutoMapper.Models
{
    public class OuterSource
    {
        public int Value { get; set; }
        public InnerSource Inner { get; set; }
    }
}