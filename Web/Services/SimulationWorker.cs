using Core.Logics;
using Core.Logics.Base;
using Core.Models;
using Core.Models.Base;
using Core.Models.Base.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Web.Services.Hubs;

namespace Web.Services
{
    public class SimulationWorker
    {
        private readonly ILogger<SimulationWorker> _logger;
        private readonly IHubContext<ApplicationHub> _hubContext;
        private readonly SpaceInfoHandler _spaceInfoHandler;
        private SimulationManager _manager;
        private Task? _managerTask;
        
        public SimulationWorker(
            ILogger<SimulationWorker> logger,
            IHubContext<ApplicationHub> hubContext,
            SpaceInfoHandler spaceInfoHandler)
        {
            _hubContext = hubContext;
            _logger = logger;
            _spaceInfoHandler = spaceInfoHandler;
            _manager = GetNewManager();
        }

        public void Start()
        {
            if (_managerTask != null)
                Stop();

            _manager = GetNewManager();
            _managerTask = Task.Run(() => _manager.Start());
        }

        public void Stop()
        {
            _manager.Stop();
            _manager.WaitStop();
            _managerTask = null;
        }

        private void ReceivingSpaceHandler(ISimulationSpace space)
        {
            var json = _spaceInfoHandler.GetJsonRepresentation(space);
            _hubContext.Clients.All.SendAsync("ReceiveSpace", json);
        }

        protected SimulationManager GetNewManager()
        {
            var manager = SimulationManager.CreateBaseManager();
            manager.AddRule(new ReceivingSpaceRule(10, ReceivingSpaceHandler));
            manager.AddRule(new PerformanceLogRule("Logs/PerformanceLog.txt", 1000));
            return manager;
        }

        public SimulationMetaInfo GetMetaInfo() => _manager.GetMetaInfo();
        public bool SwitchPause() => _manager.SwitchPause();
    }   
}