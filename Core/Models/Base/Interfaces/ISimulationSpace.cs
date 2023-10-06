using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Base.Interfaces
{
    public interface ISimulationSpace
    {
        int AddEntity(ISimulationEntity entity, int? id = null);
        ISimulationEntity GetEntity(int id);
        void RemoveEntity(ISimulationEntity entity);
        void RemoveEntity(int id);
    }
}
