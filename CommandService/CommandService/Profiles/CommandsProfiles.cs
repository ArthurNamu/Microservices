using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.Profiles;
public class CommandsProfiles : Profile
{
	public CommandsProfiles()
	{
		//source -> target
		CreateMap<Platform, PlatformReadDto>();
		CreateMap<CommandCreateDto, Command>();
		CreateMap<Command, CommandReadDto>();

	}
}
