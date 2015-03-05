using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnAutoMapper.Services
{
    public class TypeTypeConverter : ITypeConverter<string, Type>
    {
        public Type Convert(ResolutionContext context)
        {
            return context.SourceType;
        }
    }
}