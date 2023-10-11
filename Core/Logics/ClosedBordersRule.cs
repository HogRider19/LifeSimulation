using Core.Logics.Base;
using Core.Logics.Base.Interfaces;
using Core.Models.Base;
using Core.Models.Base.Interfaces;

namespace Core.Logics
{
    public class ClosedBordersRule : SimulationRule
    {
        public ClosedBordersRule() : base(ESimulationRuleType.EveryCycle) { }
        
        public override bool Apply(ISimulationSpace space, ISimulationState state)
        {
            foreach (var e in space.GetEntities())
            {
                if (e.Position.X > space.Width)
                    e.MoveTo(new Position(0, e.Position.Y));
                else if (e.Position.X < 0)
                    e.MoveTo(new Position(space.Width - 1, e.Position.Y));
                
                if (e.Position.Y > space.Height)
                    e.MoveTo(new Position(e.Position.X, 0));
                else if (e.Position.Y < 0)
                    e.MoveTo(new Position(e.Position.X, space.Height - 1));
            }
            
            return true;
        }
    }   
}