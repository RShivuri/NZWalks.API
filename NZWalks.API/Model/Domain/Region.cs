using AutoMapper;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Model.Domain
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Map Region → RegionDto
            CreateMap<Region, RegionDto>().ReverseMap(); // (Optional: Add .ReverseMap() if bidirectional mapping is needed)
            CreateMap<Region, UpdateRegionDto>().ReverseMap(); // (Optional: Add .ReverseMap() if bidirectional mapping is needed)
        }
    }
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
