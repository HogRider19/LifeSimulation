using Core.Logics;
using Microsoft.AspNetCore;
using Core.Logics.Base;
using Core.Models.Base;

namespace Web.Services
{
    public class SimulationWorker
    {
        private readonly ILogger<SimulationWorker> _logger;
        private SimulationManager _manager;
        private Task? _managerTask;
        
        public SimulationWorker(ILogger<SimulationWorker> logger)
        {
            _logger = logger;
            _manager = CreateBaseManager();
        }

        public void Start()
        {
            if (_managerTask != null)
                Stop();

            _manager = CreateBaseManager();
            _managerTask = Task.Run(() => _manager.Start());
        }

        public void Stop()
        {
            _manager.Stop();
            _manager.WaitStop();
            _managerTask = null;
        }

        protected SimulationManager CreateBaseManager()
        {
            var space = new SimulationSpace(100, 100);
            var state = SimulationState.Create();
            state.ClearAll();
            
            var manager = new SimulationManager(space, state);
            manager.AddRule(new EntityStartSpawnRule(30));
            manager.AddRule(new MealSpawnRule(1000, 10));

            return manager;
        }
        
        public bool SwitchPause() => _manager.SwitchPause();
    }   
}