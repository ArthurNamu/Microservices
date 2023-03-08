using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace CommandService.Controllers;
[Route("api/c/platform/{platformId}/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
	private readonly ICommandRepo _repository;
	private readonly IMapper _mapper;

	public CommandsController(ICommandRepo repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	[HttpGet]
	public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands(int platformId)
	{
		Console.WriteLine($"--> Hit commands platformID {platformId}");

		if (!_repository.PlatformExists(platformId))
		{
			return NotFound();
		}

		var commands = _repository.GetCommandsForPlatform(platformId);

		return Ok(_mapper.Map<IEnumerable<CommandReadDto>> (commands));
	}

	[HttpGet("{commandId}", Name = "GetCommandForPlatform" )]
	public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
	{
        Console.WriteLine($"--> Hit commands for platform {platformId} / {commandId}");

        if (!_repository.PlatformExists(platformId))
        {
            return NotFound();
        }

		var command = _repository.GetCommand(platformId, commandId);

		if(command == null) { return NotFound(); }

		return Ok(_mapper.Map<CommandReadDto>(command));

    }

	[HttpPost]
	public	ActionResult<CommandCreateDto> CreatCommandForPlatform(int platformId, CommandCreateDto commandCreate)
	{
        Console.WriteLine($"--> Hit create commands for platform {platformId}");

        if (!_repository.PlatformExists(platformId))
        {
            return NotFound();
		}

		var command = _mapper.Map<Command>(commandCreate);

		_repository.CreateCommand(platformId, command);
		_repository.SaveChanges();

		var commandReadDto = _mapper.Map<CommandReadDto>(command);

		return CreatedAtRoute(nameof(GetCommandForPlatform),
			new {platformId = platformId, commandId = commandReadDto.Id}, commandReadDto);
    }

}
