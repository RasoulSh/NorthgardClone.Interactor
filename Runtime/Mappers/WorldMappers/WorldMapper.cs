using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Northgard.GameWorld.Abstraction;
using Northgard.GameWorld.Entities;
using Northgard.Interactor.Common.Mapper;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Zenject;

namespace Northgard.Interactor.Mappers.WorldMappers
{
    [UsedImplicitly]
    public class WorldMapper : IReadOnlyMapper<World, WorldViewModel>
    {
        [Inject] private IReadOnlyMapper<Territory, TerritoryViewModel> _territoryMapper;
        [Inject] private IWorldPipelineService _pipelineService;
        
        public WorldViewModel MapToTarget(World source)
        {
            // var territories = source.territories.Select(tId => _pipelineService.FindTerritory(tId).Data);
            var territories = new List<Territory>();
            for (int x = 0; x < source.size.x; x++)
            {
                for (int y = 0; y < source.size.y; y++)
                {
                    var territoryId = source.territories[x][y];
                    if (territoryId == null)
                    {
                        continue;
                    }

                    var territory = _pipelineService.FindTerritory(territoryId).Data;
                    territories.Add(territory);
                }
            }
            return new WorldViewModel()
            {
                Id = source.id,
                Name = source.Title,
                Territories = territories.Select(_territoryMapper.MapToTarget).ToList()
            };
        }
    }
}