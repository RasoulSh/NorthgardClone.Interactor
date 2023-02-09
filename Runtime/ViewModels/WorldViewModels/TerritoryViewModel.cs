using System.Collections.Generic;
using UnityEngine;

namespace Northgard.Interactor.ViewModels.WorldViewModels
{
    public class TerritoryViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<NaturalDistrictViewModel> NaturalDistricts { get; set; }
        public delegate void TerritoryDelegate(TerritoryViewModel territoryViewModel);
    }
}