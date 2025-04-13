using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

using Object = UnityEngine.Object;


namespace Service
{
    public class Factory : IFactory
    {
        public readonly AssetData assetData;

        public event Action<Component, Type> Created;

        public Factory(AssetData assetData)
        {
            this.assetData = assetData;
        }

        public TObject Create<TObject>(string name) where TObject : Component
        {
            var createdObject = assetData.Prefabs.TryGetValue(name, out var prefab) ? Create<TObject>(prefab) : null;

            if (createdObject)
                Created?.Invoke(createdObject, createdObject.GetType());
            
            return createdObject;
        }

        public TObject CreateEmpty<TObject>() where TObject : Component
        {
            var createdObject = new GameObject(typeof(TObject).Name).AddComponent<TObject>();

            if (createdObject)
                Created?.Invoke(createdObject, createdObject.GetType());
            
            return createdObject;
        }
        
        private TObject Create<TObject>(Component prefab) where TObject : Component
        {
            var createdObject = Object.Instantiate(prefab);
            return createdObject.GetComponent<TObject>();
        }
    }
}