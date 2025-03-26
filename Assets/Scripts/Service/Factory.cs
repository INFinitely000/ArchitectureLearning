using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Object = UnityEngine.Object;


namespace Service
{
    public class Factory : IFactory
    {
        public readonly AssetData assetData;

        public IReadOnlyDictionary<string, List<Component>> CreatedObjects => _createdObjects;
        public event Action<Component, Type> Created;

        private Dictionary<string, List<Component>> _createdObjects = new Dictionary<string, List<Component>>();
        
        public Factory(AssetData assetData)
        {
            this.assetData = assetData;
        }

        public TObject Create<TObject>(string name) where TObject : Component
        {
            var createdObject = assetData.Prefabs.TryGetValue(name, out var prefab) ? Create<TObject>(prefab) : null;

            if (createdObject)
            {
                if (_createdObjects.TryGetValue(name, out var objects))
                    objects.Add(createdObject);
                else
                    _createdObjects.Add( name, new List<Component> { createdObject } );

                Created?.Invoke(createdObject, createdObject.GetType());
            }
            
            return createdObject;
        }

        public TObject GetCreatedObject<TObject>(string name) where TObject : Component
        {
            if (_createdObjects.TryGetValue(name, out var objects))
                return objects.FirstOrDefault() as TObject;

            return null;
        }

        public List<TObject> GetCreatedObjects<TObject>(string name) where TObject : Component
        {
            if (_createdObjects.TryGetValue(name, out var objects))
                return objects.Cast<TObject>().ToList();

            return null;
        }

        private TObject Create<TObject>(Component prefab) where TObject : Component
        {
            var createdObject = Object.Instantiate(prefab);
            return createdObject.GetComponent<TObject>();
        }
    }
}