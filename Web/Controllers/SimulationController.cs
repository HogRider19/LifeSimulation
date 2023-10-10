using Core.Logics.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpPost("start")]
        public ActionResult Start()
        {
            _worker.Start();
            return Ok(JsonConvert.SerializeObject(_worker.GetMetaInfo()));
        }

        [HttpPost("stop")]
        public ActionResult Stop()
        {
            _worker.Stop();
            return Ok();
        }
    
        [HttpPost("pause")]
        public ActionResult SwitchPause()
        {
            _worker.SwitchPause();
            return Ok();
        }
    }   
}