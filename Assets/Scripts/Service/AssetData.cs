using System.Collections.Generic;
using ProjectTools;
using UnityEngine;

namespace Service
{
    [CreateAssetMenu(fileName = "AssetData", menuName = "Data/AsseData")]
    public class AssetData : ScriptableObject, IAssetData
    {
        [SerializeField] private SerializableDictionary<string, GameObject> _prefabs;

        public Dictionary<string, GameObject> Prefabs => _prefabs;
    }
}
