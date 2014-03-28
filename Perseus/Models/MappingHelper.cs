using AutoMapper;
using Perseus.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Perseus.Models
{
    public static class MappingHelper
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            // Input validation.
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }

        public static void Initialize()
        {

            Mapper.CreateMap<User, EditAccountModel>()
                .ForMember(dest => dest.UId, opt => opt.MapFrom(s => s.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(s => (bool)s.Status))
                .IgnoreAllNonExisting();
            
            
            Mapper.AssertConfigurationIsValid();

        }
    }
}