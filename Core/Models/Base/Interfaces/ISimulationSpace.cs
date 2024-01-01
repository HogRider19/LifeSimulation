using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Base.Interfaces
{
    public interface ISimulationSpace
    {
        public int Width { get; }
        public int Height { get; }
        public SimulationConfig Config { get; set; }

        public int AddEntity(ISimulationEntity entity, int? id = null);
        public ISimulationEntity GetEntity(int id);
        public void RemoveEntity(ISimulationEntity entity);
        public void RemoveEntity(int id);
        public void Reset();
        
        public IEnumerable<ISimulationEntity> GetEntities();
    }
}
