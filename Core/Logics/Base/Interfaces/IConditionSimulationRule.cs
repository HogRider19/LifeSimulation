using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logics.Base.Interfaces
{
    public interface IConditionSimulationRule : ISimulationRule
    {
        public bool ReadyForApply(ISimulationState state);
    }
}
