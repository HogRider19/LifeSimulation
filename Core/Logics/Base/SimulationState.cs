using Core.Logics.Base.Interfaces;


namespace Core.Logics.Base
{
    public class SimulationState : ISimulationState
    {
        private Dictionary<ISimulationRule, Dictionary<string, object>> _state;
        private static ISimulationState? _instance;
        private static object _locker = new();
        
        private SimulationState()
        {
            _state = new Dictionary<ISimulationRule, Dictionary<string, object>>();
        }

        public static ISimulationState Create()
        {
            lock (_locker)
            {
                if (_instance == null)
                    _instance = new SimulationState();
                return _instance;
            }
        }

        public Dictionary<string, object> GetRuleContext(ISimulationRule rule)
        {
            if (!_state.ContainsKey(rule))
                _state.Add(rule, new Dictionary<string, object>());
            return _state[rule];
        }

        public void Clear(ISimulationRule rule)
        {
            if (_state.TryGetValue(rule, out var ruleContext))
                ruleContext.Clear();
        }
    }    
}
