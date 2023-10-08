using Core.Logics.Base.Interfaces;
using Core.Models.Base.Interfaces;


namespace Core.Logics.Base
{
    public abstract class SimulationRule : ISimulationRule, IConditionSimulationRule
    {
        public ESimulationRuleType Type { get; init; }

        public SimulationRule(ESimulationRuleType type = ESimulationRuleType.EveryCycle)
        {
            Type = type;
        }
        
        public virtual bool ReadyForApply(ISimulationState state)
        {
            if (Type != ESimulationRuleType.ByCondition)
                return true;
            
            throw new NotImplementedException();
        }
        
        public abstract bool Apply(ISimulationSpace space, ISimulationState state);
    }

    public enum ESimulationRuleType { OnlyStart, EveryCycle, ByCondition };
}
