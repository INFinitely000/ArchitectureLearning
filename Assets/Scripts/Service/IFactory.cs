using System.Collections.Generic;
using UnityEngine;
using System;

using Object = UnityEngine.Object;

namespace Service
{
    public interface IFactory : IService
    {
        public event Action<Component, Type> Created;
        
        public TObject Create<TObject>(string name) where TObject : Component;

        public TObject GetCreatedObject<TObject>(string name) where TObject : Component;
        
        public List<TObject> GetCreatedObjects<TObject>(string name) where TObject : Component;
    }
}