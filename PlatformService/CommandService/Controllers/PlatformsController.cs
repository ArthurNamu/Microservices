using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;
[Route("api/c/[Controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
	public PlatformsController()
	{

	}

	[HttpPost]
	public ActionResult TestInboundConnection()
	{
		Console.WriteLine("--> Inbound POST # command Service");

		return Ok("--> Inbound test from Platforms Controller");
	}
}
