using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles;
public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        // source to target
        CreateMap<Platform, PlatformReadDtos>();
        CreateMap<PlatformCreateDto, Platform>();
        CreateMap<PlatformReadDtos, PlatformPublishedDto>();
    }
}
