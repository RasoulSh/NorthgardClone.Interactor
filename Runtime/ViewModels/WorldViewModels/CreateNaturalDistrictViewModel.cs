using UnityEngine;

namespace Northgard.Interactor.ViewModels.WorldViewModels
{
    public class CreateNaturalDistrictViewModel
    {
        public NaturalDistrictPrefabViewModel Prefab { get; set; }
        public string TerritoryId { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}