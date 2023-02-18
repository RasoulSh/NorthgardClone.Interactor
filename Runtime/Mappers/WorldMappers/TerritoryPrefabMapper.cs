using System.Linq;
using JetBrains.Annotations;
using Northgard.Core.Infrastructure.Mapper;
using Northgard.Enterprise.Entities.WorldEntities;
using Northgard.GameWorld.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Zenject;

namespace Northgard.Interactor.Mappers.WorldMappers
{
    [UsedImplicitly]
    internal class TerritoryPrefabMapper : IMapper<Territory, TerritoryPrefabViewModel>
    {
        [Inject] private IWorldPipelineService _worldPipeline;
        public TerritoryPrefabViewModel MapToTarget(Territory source)
        {
            return new TerritoryPrefabViewModel()
            {
                PrefabId = source.prefabId,
                Title = source.title
            };
        }

        public Territory MapToSource(TerritoryPrefabViewModel target)
        {
            return _worldPipeline.TerritoryPrefabs.First(tp => tp.prefabId == target.PrefabId);
        }
    }
}