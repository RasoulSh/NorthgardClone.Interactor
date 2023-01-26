using System;
using UnityEngine;

namespace Northgard.Interactor.ViewModels.WorldViewModels
{
    public class NaturalDistrictViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}