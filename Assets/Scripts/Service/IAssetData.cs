using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public interface IAssetData : IService
    {
        public Dictionary<string, Component> Prefabs { get; }
    }
}