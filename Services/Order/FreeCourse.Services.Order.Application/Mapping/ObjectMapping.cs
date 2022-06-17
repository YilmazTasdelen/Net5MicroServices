using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCourse.Services.Order.Application.Mapping
{
    public static class ObjectMapping
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
