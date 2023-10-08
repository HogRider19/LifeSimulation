using Core.Logics.Base.Interfaces;
using Core.Models.Base.Interfaces;


namespace Core.Logics.Base
{
    public abstract class ByTimeSimulationRule : SimulationRule
    {
        private int _interval;
        private long _previousTime;

        public ByTimeSimulationRule(int interval) : base(ESimulationRuleType.ByCondition)
        {
            _previousTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _interval = interval;
        }

        public override bool ReadyForApply(ISimulationState state)
        {
            if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - _previousTime > _interval)
            {
                _previousTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                return true;
            }

            return false;
        }
    }    
}
