using Core.Logics.Base;
using Core.Logics.Base.Interfaces;
using Core.Models.Base.Interfaces;


namespace Core.Logics
{
    public class ReceivingSpaceRule : ByTimeSimulationRule
    {
        private readonly Action<ISimulationSpace> _action;
        
        public ReceivingSpaceRule(int interval, Action<ISimulationSpace> action) : base(interval)
        {
            _action = action;
        }

        public override bool Apply(ISimulationSpace space, ISimulationState state)
        {
            try
            {
                _action.Invoke(space);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }   
}