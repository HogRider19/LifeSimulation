using Core.Logics.Base.Interfaces;
using Core.Models.Base.Interfaces;

namespace Core.Logics.Base
{
    public abstract class SimulationRule : ISimulationRule
    {
        public SimulationRuleType Type { get; init; }

        public SimulationRule(SimulationRuleType type = SimulationRuleType.EveryCicle)
        {
            Type = type;
        }

        public abstract bool Apply(ISimulationSpace space);
    }

    public enum SimulationRuleType { OnlyStart, EveryCicle, ByCondition };
}
