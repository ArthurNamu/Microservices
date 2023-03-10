using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;
    private readonly IMessageBusClient _messageBusClient;

    public PlatformController(
        IPlatformRepo repository, 
        IMapper mapper,
        ICommandDataClient commandDataClient,
        IMessageBusClient messageBusClient)
    {
        _repository = repository;
        _mapper = mapper;
        _commandDataClient = commandDataClient;
        _messageBusClient = messageBusClient;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDtos>> GetPlatforms()
    {

        var platformItems = _repository.GetAllPlatforms();

        if(platformItems.Any())
         return Ok(_mapper.Map<IEnumerable<PlatformReadDtos>>(platformItems));

        return NotFound();
    }

    [HttpGet("{id}", Name = "GetPlatformsByID")]
    public ActionResult<PlatformReadDtos> GetPlatformsByID(int id)
    {

        var platformItem = _repository.GetPlatformById(id);

        if(platformItem != null)
        {
            return Ok(_mapper.Map<PlatformReadDtos>(platformItem));
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDtos>> CreatePlatform(PlatformCreateDto platformCreateDto)
    {

        var platformModel = _mapper.Map<Platform>(platformCreateDto);
        _repository.CreatePlatform(platformModel);
        _repository.SaveChanges();

        var platformReadDto = _mapper.Map<PlatformReadDtos>(platformModel);
        //Send Sync Message

        try
        {
           await  _commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Could not send synchronously", ex.Message);
        }

        //Send async Message
        try
        {
            var platformPuclishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
            platformPuclishedDto.Event = "Platform_published";
            _messageBusClient.PublishNewPlatform(platformPuclishedDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine("--> Could not send Asynchronously");
        }

        return CreatedAtRoute(nameof(GetPlatformsByID), new { Id = platformReadDto.Id }, platformReadDto);

    }

}
