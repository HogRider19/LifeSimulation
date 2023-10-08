using Core.Logics.Base;
using Core.Logics.Base.Interfaces;
using Core.Models;
using Core.Models.Base;
using Core.Models.Base.Interfaces;


namespace Core.Logics
{
    public class EntityStartSpawnRule : SimulationRule
    {
        private int _count;

        public EntityStartSpawnRule(int count = 30) : base(ESimulationRuleType.OnlyStart)
        {
            _count = count;
        }
        
        public override bool Apply(ISimulationSpace space, ISimulationState state)
        {
            for (var i = 0; i < _count; i++)
            {
                var rnd = new Random();

                foreach (EPointType pointType in Enum.GetValues(typeof(EPointType)))
                {
                    space.AddEntity(new Point(
                        Position.CreateRandom(space.Width, space.Height),
                        rnd.NextDouble() * Math.PI * 2, pointType));
                }
                
            }

            return true;
        }
    }   
}