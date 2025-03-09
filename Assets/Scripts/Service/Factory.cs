using UnityEngine;

namespace Service
{
    public class Factory : IFactory
    {
        public AssetData assetData;

        public Factory(AssetData assetData)
        {
            this.assetData = assetData;
        }

        public TObject Create<TObject>(string name) where TObject : Object
        {
            var createdObject = assetData.Prefabs.TryGetValue(name, out var prefab) ? Create(prefab) : null;
            return createdObject?.GetComponent<TObject>();
        }

        private TObject Create<TObject>(TObject prefab) where TObject : Object
        {
            var createdObject = Object.Instantiate(prefab);
            return createdObject;
        }
    }
}