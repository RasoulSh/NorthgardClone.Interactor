using System.Collections.Generic;
using Northgard.Core.Enums;
using Northgard.Core.GameObjectBase;
using Northgard.GameWorld.Abstraction.Behaviours;
using Northgard.Interactor.ViewModels.WorldViewModels;
using UnityEngine;

namespace Northgard.Interactor.Abstraction
{
    public interface IWorldEditorController
    {
        public WorldViewModel CurrentWorld { get; }
        public IEnumerable<WorldPrefabViewModel> WorldPrefabs { get; }
        public IEnumerable<TerritoryPrefabViewModel> TerritoryPrefabs { get; }
        public IEnumerable<NaturalDistrictPrefabViewModel> NaturalDistrictPrefabs { get; }
        event TerritoryViewModel.TerritoryDelegate OnTerritoryAdded;
        event NaturalDistrictViewModel.NaturalDistrictDelegate OnNaturalDistrictAdded;
        event LoadDelegate OnWorldChanged;
        void SelectWorld(SelectWorldViewModel selectData);
        TerritoryViewModel SelectFirstTerritory(SelectFirstTerritoryViewModel selectData);
        TerritoryViewModel NewTerritory(CreateTerritoryViewModel createData);
        void NewNaturalDistrict(CreateNaturalDistrictViewModel createData);
        void RemoveTerritory(string id);
        void RemoveNaturalDistrict(string id);
        void SaveWorld(string savedName);
        WorldViewModel LoadWorld(string savedName);
        TC AddComponentToTerritory<TC>(string territoryId) where TC : Component;
        TC AddComponentToNaturalDistrict<TC>(string naturalDistrictId) where TC : Component;
        GameObject GenerateFakeNaturalDistrict(string prefabId);
        IEnumerable<WorldDirection> GetTerritoryAvailableDirections(string territoryId);
        void RepositionNaturalDistrict(string naturalDistrictId, string territoryId, Vector3 newPosition);
        public delegate void LoadDelegate();
    }
}