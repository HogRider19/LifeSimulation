using Core.Logics.Base.Interfaces;
using Core.Models.Base;
using Core.Models.Base.Interfaces;


namespace Core.Logics.Base
{
    public class SimulationManager : ISimulationManager
    {
        public ESimulationManagerState ManagerState { get; protected set; }
        public ISimulationState SimulationState { get; protected set; }
        public SimulationConfig Config { get; init; }

        private List<ISimulationRule> _rules;
        private ISimulationSpace _space;

        public SimulationManager(
            ISimulationSpace space,
            ISimulationState simulationState,
            List<ISimulationRule>? rules = null,
            SimulationConfig? config = null)
        {
            ManagerState = ESimulationManagerState.NotStarted;
            SimulationState = simulationState;
            Config = config ?? new SimulationConfig();
            _rules = rules ?? new List<ISimulationRule>();
            _space = space;

            _space.Config = Config;
        }

        public void Start(bool resetSpase = true)
        {
            if (ManagerState != ESimulationManagerState.NotStarted) return;
            if (resetSpase) _space.Reset();

            var onlyStartRules = _rules.Where(r => r.Type == ESimulationRuleType.OnlyStart).ToList();
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
                        /* ignored */
                        Console.WriteLine(exc + Environment.NewLine);
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

        public SimulationMetaInfo GetMetaInfo()
        {
            return new SimulationMetaInfo()
            {
                SpaceWidth = _space.Width,
                SpaceHeight = _space.Height
            };
        }

        public static SimulationManager CreateBaseManager()
        {
            var space = new SimulationSpace(100, 100);
            var state = Core.Logics.Base.SimulationState.Create();
            state.ClearAll();
            
            var manager = new SimulationManager(space, state);
            manager.AddRule(new EntityStartSpawnRule(30));
            manager.AddRule(new MealSpawnRule(1000, 10));
            manager.AddRule(new ClosedBordersRule());
            manager.AddRule(new HpHandlerRule(1000));
            manager.AddRule(new PointReproductionRule(500));

            var movePointRule = new MovePointRule();
            var findTargetRule = new FindTargetRule(2000, movePointRule);
            manager.AddRule(findTargetRule);
            manager.AddRule(movePointRule);

            return manager;
        }

        public void Stop() => ManagerState = ESimulationManagerState.Stopped;
        public void AddRule(ISimulationRule rule) => _rules.Add(rule);
        public void AddRule(IEnumerable<ISimulationRule> rules) => _rules.AddRange(rules);
        public void WaitStop() { while (ManagerState != ESimulationManagerState.NotStarted) { } }
    }

    public enum ESimulationManagerState { Started, NotStarted, Stopped, Waiting }

    public struct SimulationMetaInfo
    {
        public int SpaceWidth { get; set; }
        public int SpaceHeight { get; set; }
    }
}
