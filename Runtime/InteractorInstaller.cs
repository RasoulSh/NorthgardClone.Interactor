using Northgard.Core.Infrastructure.Mapper;
using Northgard.Enterprise.Entities.WorldEntities;
using Northgard.Interactor.Abstraction;
using Northgard.Interactor.Controllers;
using Northgard.Interactor.Mappers.WorldMappers;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Zenject;

namespace Northgard.Interactor
{
    public class InteractorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            #region Mappers
            Container.Bind<IMapper<World, WorldPrefabViewModel>>().To<WorldPrefabMapper>().FromNew().AsSingle();
            Container.Bind<IMapper<NaturalDistrict, NaturalDistrictPrefabViewModel>>().To<NaturalDistrictPrefabMapper>().FromNew().AsSingle();
            Container.Bind<IMapper<Territory, TerritoryPrefabViewModel>>().To<TerritoryPrefabMapper>().FromNew().AsSingle();
            Container.Bind<IReadOnlyMapper<NaturalDistrict, NaturalDistrictViewModel>>().To<NaturalDistrictMapper>()
                .FromNew().AsSingle();
            Container.Bind<IReadOnlyMapper<Territory, TerritoryViewModel>>().To<TerritoryMapper>().FromNew().AsSingle();
            Container.Bind<IReadOnlyMapper<World, WorldViewModel>>().To<WorldMapper>().FromNew().AsSingle();
            #endregion

            #region Controllers
            Container.Bind<IWorldEditorController>().To<WorldEditorController>().FromNew().AsSingle();
            #endregion
        }
    }
}