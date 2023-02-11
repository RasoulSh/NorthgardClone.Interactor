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
    internal class WorldPrefabMapper : IMapper<World, WorldPrefabViewModel>
    {
        [Inject] private IWorldPipelineService _worldPipeline;
        public WorldPrefabViewModel MapToTarget(World source)
        {
            return new WorldPrefabViewModel()
            {
                PrefabId = source.PrefabId,
                Title = source.Title
            };
        }

        public World MapToSource(WorldPrefabViewModel target)
        {
            return _worldPipeline.WorldPrefabs.First(wp => wp.PrefabId == target.PrefabId);
        }
    }
}