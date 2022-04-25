using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;
using RedisAPI.Models;

namespace RedisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repo;

        public PlatformsController(IPlatformRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetPlatforms()
        {
            return Ok(_repo.GetAllPlatforms());
        }        

        [HttpGet("{id}", Name="GetPlatformById")]    
        public ActionResult<IEnumerable<Platform>> GetPlatformById(string id)
        {
            var platform = _repo.GetPlatformById(id);

            if(platform !=null)
            {
                return Ok(platform);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Platform> CreatePlatform(Platform platform)
        {
            _repo.CreatePlatform(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new {Id = platform.id}, platform);
        }
    }
}