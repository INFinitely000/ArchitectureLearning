using System.Collections.Generic;
using Service;
using UnityEngine;

namespace Data
{
    public interface IAssetData : IService
    {
        public Dictionary<string, Component> Prefabs { get; }
    }
}