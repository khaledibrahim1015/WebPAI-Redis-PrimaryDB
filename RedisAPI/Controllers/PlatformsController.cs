using Microsoft.AspNetCore.Mvc;

namespace RedisAPI;
[ApiController]
[Route("api/[controller]")]
public class PlatformsController : ControllerBase
{

    private readonly IPlatformService _service ;

    public PlatformsController(IPlatformService service)
    {
        _service = service;
    }

    [HttpGet]

    public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
    {

        return Ok(_service.GetAllPlatform());

    }


    [HttpGet("{id}",Name ="GetPlatfromById")]
    public ActionResult<Platform> GetPlatfromById(string id)
    {
        var plat =  _service.GetPlatformById(id);

        if(plat != null )
            return Ok(plat);
        
        return NotFound();


    }

    [HttpPost]

    public ActionResult<Platform> CreatePlatform(Platform platform)
    {

        _service.CreatePlatform(platform);

        return CreatedAtRoute("GetPlatfromById",new {id = platform.Id},platform);
        

    }






}