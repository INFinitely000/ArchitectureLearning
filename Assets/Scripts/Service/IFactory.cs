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

        public TObject CreateEmpty<TObject>() where TObject : Component;
    }
}