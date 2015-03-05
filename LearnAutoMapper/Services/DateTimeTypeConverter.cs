using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnAutoMapper.Services
{
    public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(ResolutionContext context)
        {
            return System.Convert.ToDateTime(context.SourceValue);
        }
    }
}