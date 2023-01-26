using Northgard.GameWorld.Entities;
using Northgard.Interactor.Common.Mapper;
using Northgard.Interactor.Mappers.WorldMappers;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Zenject;

namespace Northgard.Interactor
{
    public class InteractorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMapper<World, WorldPrefabViewModel>>().To<WorldPrefabMapper>().FromNew().AsSingle();
            Container.Bind<IMapper<Territory, TerritoryPrefabViewModel>>().To<TerritoryPrefabMapper>().FromNew().AsSingle();
            Container.Bind<IMapper<NaturalDistrict, NaturalDistrictPrefabViewModel>>().To<NaturalDistrictPrefabMapper>().FromNew().AsSingle();
        }
    }
}