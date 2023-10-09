using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers
{
    [Route("[controller]")]
    public class MapController : Controller
    {
        private readonly ILogger<MapController> _logger;
        
        public MapController(ILogger<MapController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public ViewResult Map()
        {
            return View();
        }
    }
}
