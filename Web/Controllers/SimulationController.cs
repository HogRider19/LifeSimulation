using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimulationController : ControllerBase
    {
        private readonly ILogger<SimulationController> _logger;
        private readonly SimulationWorker _worker;
        
        public SimulationController(ILogger<SimulationController> logger, SimulationWorker worker)
        {
            _logger = logger;
            _worker = worker;
        }

        [Route("start")]
        public ActionResult Start()
        {
            _worker.Start();
            return Ok();
        }

        [Route("stop")]
        public ActionResult Stop()
        {
            _worker.Stop();
            return Ok();
        }
    
        [Route("pause")]
        public ActionResult SwitchPause()
        {
            _worker.SwitchPause();
            return Ok();
        }
    }   
}