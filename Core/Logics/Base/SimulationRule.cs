using Core.Logics.Base.Interfaces;
using Core.Models.Base.Interfaces;


namespace Core.Logics.Base
{
    public abstract class SimulationRule : ISimulationRule, IConditionSimulationRule
    {
        public SimulationRuleType Type { get; init; }

        public SimulationRule(SimulationRuleType type = SimulationRuleType.EveryCycle)
        {
            Type = type;
        }
        
        public virtual bool ReadyForApply(ISimulationState state)
        {
            if (Type != SimulationRuleType.ByCondition)
                return true;
            
            throw new NotImplementedException();
        }
        
        public abstract bool Apply(ISimulationSpace space, ISimulationState state);
    }

    public enum SimulationRuleType { OnlyStart, EveryCycle, ByCondition };
}
