using Core.Logics.Base.Interfaces;
using Core.Models.Base.Interfaces;


namespace Core.Logics.Base
{
    public abstract class ByTimeSimulationRule : SimulationRule
    {
        protected int Interval;
        private long _previousTime;

        public ByTimeSimulationRule(int interval) : base(ESimulationRuleType.ByCondition)
        {
            _previousTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Interval = interval;
        }

        public override bool ReadyForApply(ISimulationState state)
        {
            if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - _previousTime > Interval)
            {
                _previousTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                return true;
            }

            return false;
        }
    }    
}
