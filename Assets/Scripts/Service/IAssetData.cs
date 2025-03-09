using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public interface IAssetData : IService
    {
        public Dictionary<string, GameObject> Prefabs { get; }
    }
}