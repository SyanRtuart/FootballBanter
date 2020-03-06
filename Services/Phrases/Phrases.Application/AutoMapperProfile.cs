using System.Reflection;
using AutoMapper;
using Base.Application.Common.Mappings;

namespace Phrases.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            LoadStandardMappings();
            LoadCustomMappings();
            LoadConverters();
        }

        private void LoadConverters()
        {
        }

        private void LoadStandardMappings()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var mapsFrom = MappingProfile.MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom) CreateMap(map.Source, map.Destination).ReverseMap();
        }

        private void LoadCustomMappings()
        {
            var mapsFrom = MappingProfile.MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom) map.CreateMappings(this);
        }
    }
}
