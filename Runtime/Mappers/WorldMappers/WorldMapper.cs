using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Northgard.Core.Infrastructure.Mapper;
using Northgard.Enterprise.Entities.WorldEntities;
using Northgard.GameWorld.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using UnityEngine;
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
                    var point = new Vector2(x, y);
                    var territoryId = source.territories.Find(t => t.point == point).id;
                    if (string.IsNullOrEmpty(territoryId))
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
                Name = source.title,
                Territories = territories.Select(_territoryMapper.MapToTarget).ToList()
            };
        }
    }
}