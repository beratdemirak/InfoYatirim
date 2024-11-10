using Microsoft.AspNetCore.Mvc;

namespace InfoYatirim.Consumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly CacheService cache;
        private readonly ConsumerService service;

        public HomeController(CacheService cache, ConsumerService service)
        {
            this.cache = cache;
            this.service = service;
        }

        [HttpGet(Name = "GetData")]
        public async Task<IActionResult> Get()
        {
            await service.Create();
            return Ok(cache.GetData());
        }
    }
}
