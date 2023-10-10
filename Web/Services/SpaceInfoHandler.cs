using Core.Models;
using Core.Models.Base.Interfaces;
using Newtonsoft.Json;

namespace Web.Services
{
    public class SpaceInfoHandler
    {
        private readonly ILogger<SpaceInfoHandler> _logger;

        public SpaceInfoHandler(ILogger<SpaceInfoHandler> logger)
        {
            _logger = logger;
        }

        public string GetJsonRepresentation(ISimulationSpace space) 
        {
            var data = new List<List<double>>();
            foreach (var entity in space.GetEntities())
            {
                var entityList = new List<double>();
                entityList.Add(entity.Position.X);
                entityList.Add(entity.Position.Y);
                
                if (entity.GetType() == typeof(Meal))
                    entityList.Add(3);
                else if (entity.GetType() == typeof(Point))
                {
                    var point = (Point)entity;
                    entityList.Add((int)point.Type.GetTypeCode());
                }
                else
                    throw new ArgumentException("Undefined entity type");
                
                data.Add(entityList);
            }
            
            return JsonConvert.SerializeObject(data);
        }
    }
}