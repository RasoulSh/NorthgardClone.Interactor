using System;
using System.Collections.Generic;
using System.Linq;
using Northgard.GameWorld.Abstraction;
using Northgard.GameWorld.Entities;
using Northgard.Interactor.Abstraction;
using Northgard.Interactor.Common.Mapper;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Zenject;

namespace Northgard.Interactor.Controllers
{
    internal class WorldEditorController : IWorldEditorController
    {
        [Inject] private IWorldEditorService _worldEditor;
        [Inject] private IWorldPipelineService _worldPipeline;
        [Inject] private IMapper<World, WorldPrefabViewModel> _worldPrefabMapper;
        [Inject] private IMapper<Territory, TerritoryPrefabViewModel> _territoryPrefabMapper;
        [Inject] private IMapper<NaturalDistrict, NaturalDistrictPrefabViewModel> _naturalDistrictPrefabMapper;

        public IEnumerable<WorldPrefabViewModel> WorldPrefabs =>
            _worldPipeline.WorldPrefabs.Select(_worldPrefabMapper.MapToTarget);

        public IEnumerable<TerritoryPrefabViewModel> TerritoryPrefabs =>
            _worldPipeline.TerritoryPrefabs.Select(_territoryPrefabMapper.MapToTarget);
        
        public IEnumerable<NaturalDistrictPrefabViewModel> NaturalDistrictPrefabs =>
            _worldPipeline.NaturalDistrictPrefabs.Select(_naturalDistrictPrefabMapper.MapToTarget);

        public void SelectWorld(SelectWorldViewModel selectData)
        {
            _worldPipeline.SetWorld(_worldPrefabMapper.MapToSource(selectData.Prefab));
            _worldEditor.World = _worldPipeline.World;
        }

        public TerritoryViewModel SelectFirstTerritory(SelectFirstTerritoryViewModel selectData)
        {
            throw new NotImplementedException();
        }

        public TerritoryViewModel NewTerritory(CreateTerritoryViewModel createData)
        {
            throw new System.NotImplementedException();
        }

        public NaturalDistrictViewModel NewNaturalDistrict(CreateNaturalDistrictViewModel createData)
        {
            throw new System.NotImplementedException();
        }

        public WorldViewModel LoadWorld(string savedName)
        {
            throw new System.NotImplementedException();
        }
    }
}