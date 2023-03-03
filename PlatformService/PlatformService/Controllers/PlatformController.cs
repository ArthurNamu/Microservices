using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;

    public PlatformController(IPlatformRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;

    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDtos>> GetPlatforms()
    {

        var platformItems = _repository.GetAllPlatforms();

        if(!platformItems.Any())
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
    public ActionResult<PlatformReadDtos> CreatePlatform(PlatformCreateDto platformCreateDto)
    {

        var platformModel = _mapper.Map<Platform>(platformCreateDto);
        _repository.CreatePlatform(platformModel);
        _repository.SaveChanges();

        var platformReadDto = _mapper.Map<PlatformReadDtos>(platformModel);

        return CreatedAtRoute(nameof(GetPlatformsByID), new { Id = platformReadDto.Id }, platformReadDto);

    }

}
