using Core.Logics.Base.Interfaces;
using Core.Models.Base.Interfaces;


namespace Core.Logics.Base
{
    public class SimulationManager : ISimulationManager
    {
        public ESimulationManagerState ManagerState { get; protected set; }
        public ISimulationState SimulationState { get; protected set; }

        private List<ISimulationRule> _rules;
        private ISimulationSpace _space;

        public SimulationManager(
            ISimulationSpace space,
            ISimulationState simulationState,
            List<ISimulationRule>? rules = null)
        {
            ManagerState = ESimulationManagerState.NotStarted;
            SimulationState = simulationState;
            _rules = rules ?? new List<ISimulationRule>();
            _space = space;
        }

        public void Start(bool resetSpase = true)
        {
            if (ManagerState != ESimulationManagerState.NotStarted) return;
            if (resetSpase) _space.Reset();

            var onlyStartRules = _rules.Where(r => r.Type == ESimulationRuleType.OnlyStart);
            _rules.RemoveAll(r => onlyStartRules.Contains(r));
            foreach (var onlyStartRule in onlyStartRules)
                onlyStartRule.Apply(_space, SimulationState);

            while (ManagerState != ESimulationManagerState.Stopped)
            {
                if (ManagerState == ESimulationManagerState.Waiting) continue;

                foreach (var rule in _rules)
                {
                    try
                    {
                        if (rule.Type == ESimulationRuleType.EveryCycle)
                            rule.Apply(_space, SimulationState);

                        if (rule.Type == ESimulationRuleType.ByCondition)
                        {
                            if (rule is IConditionSimulationRule conditionRule)
                            {
                                if (conditionRule.ReadyForApply(SimulationState))
                                    conditionRule.Apply(_space, SimulationState);
                            }
                            
                        }
                    }
                    catch (Exception exc)
                    {
                        // ignored
                    }
                }
            }

            ManagerState = ESimulationManagerState.NotStarted;
        }

        public bool SwitchPause()
        {
            if (ManagerState != ESimulationManagerState.Started
                && ManagerState != ESimulationManagerState.Waiting)
                return false;
            
            if (ManagerState == ESimulationManagerState.Started)
            {
                ManagerState = ESimulationManagerState.Waiting;
                return true;
            }
            
            ManagerState = ESimulationManagerState.Started;
            return false;
        }

        public void Stop() => ManagerState = ESimulationManagerState.Stopped;
        public void AddRule(ISimulationRule rule) => _rules.Add(rule);
        public void AddRule(IEnumerable<ISimulationRule> rules) => _rules.AddRange(rules);
    }

    public enum ESimulationManagerState { Started, NotStarted, Stopped, Waiting }
}
