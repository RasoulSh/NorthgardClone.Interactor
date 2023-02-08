﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Northgard.GameWorld.Abstraction;
using Northgard.GameWorld.Abstraction.Behaviours;
using Northgard.GameWorld.Data;
using Northgard.GameWorld.Entities;
using Northgard.Interactor.Abstraction;
using Northgard.Interactor.Common.Mapper;
using Northgard.Interactor.Common.WorldUtilities;
using Northgard.Interactor.Enums.WorldEnums;
using Northgard.Interactor.ViewModels.WorldViewModels;
using UnityEngine;
using Zenject;
using ILogger = Northgard.Core.Abstraction.Logger.ILogger;

namespace Northgard.Interactor.Controllers
{
    [UsedImplicitly]
    internal class WorldEditorController : IWorldEditorController
    {
        [Inject] private ILogger _logger;
        [Inject] private IWorldPipelineService _worldPipeline;
        [Inject] private IMapper<World, WorldPrefabViewModel> _worldPrefabMapper;
        [Inject] private IMapper<Territory, TerritoryPrefabViewModel> _territoryPrefabMapper;
        [Inject] private IMapper<NaturalDistrict, NaturalDistrictPrefabViewModel> _naturalDistrictPrefabMapper;
        [Inject] private IReadOnlyMapper<World, WorldViewModel> _worldMapper;
        [Inject] private IReadOnlyMapper<Territory, TerritoryViewModel> _territoryMapper;
        [Inject] private IReadOnlyMapper<NaturalDistrict, NaturalDistrictViewModel> _naturalDistrictMapper;
        private IWorldEditorService _worldEditor;

        public IEnumerable<WorldPrefabViewModel> WorldPrefabs =>
            _worldPipeline.WorldPrefabs.Select(_worldPrefabMapper.MapToTarget);

        public IEnumerable<TerritoryPrefabViewModel> TerritoryPrefabs =>
            _worldPipeline.TerritoryPrefabs.Select(_territoryPrefabMapper.MapToTarget);
        
        public IEnumerable<NaturalDistrictPrefabViewModel> NaturalDistrictPrefabs =>
            _worldPipeline.NaturalDistrictPrefabs.Select(_naturalDistrictPrefabMapper.MapToTarget);

        public event TerritoryViewModel.TerritoryDelegate OnTerritoryAdded;
        public event NaturalDistrictViewModel.NaturalDistrictDelegate OnNaturalDistrictAdded;

        [Inject]
        private void Init(IWorldEditorService worldEditor)
        {
            _worldEditor = worldEditor;
            _worldEditor.OnTerritoryAdded += _OnTerritoryAdded;
            _worldEditor.OnNaturalDistrictAdded += _OnNaturalDistrictAdded;
        }

        private void _OnTerritoryAdded(ITerritoryBehaviour territoryBehaviour)
        {
            OnTerritoryAdded?.Invoke( _territoryMapper.MapToTarget(territoryBehaviour.Data));
        }
        
        private void _OnNaturalDistrictAdded(ITerritoryBehaviour territory, INaturalDistrictBehaviour naturalDistrict)
        {
            var territoryVm = _territoryMapper.MapToTarget(territory.Data);
            var naturalDistrictVm = _naturalDistrictMapper.MapToTarget(naturalDistrict.Data);
            OnNaturalDistrictAdded?.Invoke(territoryVm, naturalDistrictVm);
        }

        public void SelectWorld(SelectWorldViewModel selectData)
        {
            if (selectData == null)
            {
                _logger.LogError("World select view data is null", this);
                return;
            }

            if (selectData.Prefab == null)
            {
                _logger.LogError("World select view data prefab is null", this);
                return;
            }
            _worldPipeline.SetWorld(_worldPrefabMapper.MapToSource(selectData.Prefab));
            _worldEditor.World = _worldPipeline.World;
        }

        public TerritoryViewModel SelectFirstTerritory(SelectFirstTerritoryViewModel selectData)
        {
            if (selectData == null)
            {
                _logger.LogError("First territory select view data is null", this);
                return null;
            }
            if (selectData.Prefab == null)
            {
                _logger.LogError("First territory select view data prefab is null", this);
                return null;
            }
            if (_worldPipeline.Territories.Any())
            {
                Debug.LogError("You cannot select the first territory while there are any other territories");
                return null;
            }

            var territoryPrefab = _territoryPrefabMapper.MapToSource(selectData.Prefab);
            var newTerritory = _worldPipeline.InstantiateTerritory(territoryPrefab);

            var worldBounds = _worldPipeline.World.Data.Bounds;
            var territoryBounds = territoryPrefab.Bounds;
            var startPosition = worldBounds.min + territoryBounds.extents + Vector3.up * (worldBounds.size.y + territoryBounds.size.y + 0.1f);
            newTerritory.SetPosition(startPosition);
            _worldPipeline.World.AddTerritory(newTerritory, Vector2Int.zero);
            return _territoryMapper.MapToTarget(newTerritory.Data);
        }

        public TerritoryViewModel NewTerritory(CreateTerritoryViewModel createData)
        {
            if (createData == null)
            {
                _logger.LogError("First territory select view data is null", this);
                return null;
            }
            if (createData.Prefab == null)
            {
                _logger.LogError("First territory select view data prefab is null", this);
                return null;
            }
            if (string.IsNullOrEmpty(createData.SourceTerritoryId))
            {
                _logger.LogError("You should provide a source territory id to put the new territory besides", this);
                return null;
            }
            var sourceTerritory = _worldPipeline.FindTerritory(createData.SourceTerritoryId);
            var territoryPrefab = _territoryPrefabMapper.MapToSource(createData.Prefab);
            var newTerritory = _worldPipeline.InstantiateTerritory(territoryPrefab);

            var sourceBounds = sourceTerritory.Data.Bounds;
            var territoryBounds = territoryPrefab.Bounds;
            
            Vector3 positionShift = Vector2.zero;
            Vector2Int pointShift = Vector2Int.zero;

            switch (createData.Direction)
            {
                case WorldDirection.East:
                    positionShift.x += sourceBounds.extents.x + territoryBounds.extents.x;
                    pointShift.x++;
                    break;
                case WorldDirection.North:
                    positionShift.z += sourceBounds.extents.z + territoryBounds.extents.z;
                    pointShift.y++;
                    break;
                case WorldDirection.South:
                    positionShift.z -= sourceBounds.extents.z + territoryBounds.extents.z;
                    pointShift.y--;
                    break;
                case WorldDirection.West:
                    positionShift.x -= sourceBounds.extents.x + territoryBounds.extents.x;
                    pointShift.x--;
                    break;
            }
            newTerritory.SetPosition(sourceTerritory.Data.position + positionShift);
            // sourceTerritory.AddTerritoryConnection(newTerritory, (GameWorld.Enums.WorldDirection)createData.Direction);
            // newTerritory.AddTerritoryConnection(sourceTerritory, (GameWorld.Enums.WorldDirection)WorldDirectionUtil.OpposeDirection(createData.Direction));
            _worldPipeline.World.AddTerritory(newTerritory, sourceTerritory.Data.pointInWorld + pointShift);
            return _territoryMapper.MapToTarget(newTerritory.Data);
        }

        public NaturalDistrictViewModel NewNaturalDistrict(CreateNaturalDistrictViewModel createData)
        {
            var territory = _worldPipeline.FindTerritory(createData.TerritoryId);
            var naturalDistrictPrefab = _naturalDistrictPrefabMapper.MapToSource(createData.Prefab);
            var newNaturalDistrict = _worldPipeline.InstantiateNaturalDistrict(naturalDistrictPrefab);
            newNaturalDistrict.SetPosition(territory.Data.position +  Vector3.up * newNaturalDistrict.Data.Bounds.extents.y);
            territory.AddNaturalDistrict(newNaturalDistrict);
            return _naturalDistrictMapper.MapToTarget(newNaturalDistrict.Data);
        }

        private const string SavedNamePrepend = "WorldMap_";

        public void SaveWorld(string savedName)
        {
            var worldDataset = new WorldDataset()
            {
                world = _worldPipeline.World.Data,
                territories = _worldPipeline.Territories.Select(t => t.Data).ToList(),
                naturalDistricts = _worldPipeline.NaturalDistricts.Select(nd => nd.Data).ToList()
            };
            var worldJson = JsonUtility.ToJson(worldDataset);
            PlayerPrefs.SetString(SavedNamePrepend + savedName, worldJson);
        }

        public WorldViewModel LoadWorld(string savedName)
        {
            var worldJson = PlayerPrefs.GetString(SavedNamePrepend + savedName);
            var worldData = JsonUtility.FromJson<WorldDataset>(worldJson);
            _worldPipeline.SetWorld(worldData.world);
            _worldEditor.World = _worldPipeline.World;
            foreach (var territory in worldData.territories)
            {
                _worldPipeline.InstantiateTerritory(territory);
            }
            foreach (var naturalDistrict in worldData.naturalDistricts)
            {
                _worldPipeline.InstantiateNaturalDistrict(naturalDistrict);
            }
            _worldPipeline.Initialize();
            return _worldMapper.MapToTarget(_worldPipeline.World.Data);
        }

        public TC AddComponentToTerritory<TC>(string territoryId) where TC : Component
        {
            var territory = _worldPipeline.FindTerritory(territoryId);
            return territory.AddComponent<TC>();
        }

        public TC AddComponentToNaturalDistrict<TC>(string naturalDistrictId) where TC : Component
        {
            var naturalDistrict = _worldPipeline.FindNaturalDistrict(naturalDistrictId);
            return naturalDistrict.AddComponent<TC>();
        }

        public IEnumerable<WorldDirection> GetTerritoryAvailableDirections(string territoryId)
        {
            var filteredDirections = (Enum.GetValues(typeof(GameWorld.Enums.WorldDirection)) as GameWorld.Enums.WorldDirection[]).ToList();
            var world = _worldPipeline.World;
            var worldTerritories = world.Territories;
            var territory = _worldPipeline.FindTerritory(territoryId);
            var pointInWorld = territory.Data.pointInWorld;
            if (pointInWorld.x == world.Data.size.x - 1 || worldTerritories[pointInWorld.x + 1][pointInWorld.y] != null)
            {
                filteredDirections.Remove(GameWorld.Enums.WorldDirection.East);
            }
            if (pointInWorld.x == 0 || worldTerritories[pointInWorld.x - 1][pointInWorld.y] != null)
            {
                filteredDirections.Remove(GameWorld.Enums.WorldDirection.West);
            }
            if (pointInWorld.y == world.Data.size.y - 1 || worldTerritories[pointInWorld.x][pointInWorld.y + 1] != null)
            {
                filteredDirections.Remove(GameWorld.Enums.WorldDirection.North);
            }

            if (pointInWorld.y == 0 || worldTerritories[pointInWorld.x][pointInWorld.y - 1] != null)
            {
                filteredDirections.Remove(GameWorld.Enums.WorldDirection.South);
            }
            return filteredDirections.Select(d => (WorldDirection)d);
        }
    }
}