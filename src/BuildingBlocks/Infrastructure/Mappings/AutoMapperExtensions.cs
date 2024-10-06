﻿using AutoMapper;
using System.Reflection;

namespace Infrastructure.Mappings
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> expression)
        {
            var flag = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flag);

            foreach (var property in destinationProperties)
                if (sourceType.GetProperty(property.Name, flag) == null)
                    expression.ForMember(property.Name, opt => opt.Ignore());
            return expression;
        }
    }
}