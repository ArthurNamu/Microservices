using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;
[Route("api/c/[Controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
	private readonly ICommandRepo _repository;
	private readonly IMapper _mapper;

	public PlatformsController(ICommandRepo repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	[HttpGet]
	public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
	{
		Console.WriteLine("--> Getting Platforms from Command Service");
		var platformItems = _repository.GetPlatforms();

		return Ok(_mapper.Map <IEnumerable<PlatformReadDto>>(platformItems));
	}

	[HttpPost]
	public ActionResult TestInboundConnection()
	{
		Console.WriteLine("--> Inbound POST # command Service");

		return Ok("--> Inbound test from Platforms Controller");
	}
}
