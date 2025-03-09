using UnityEngine;

namespace Service
{
    public interface IFactory : IService
    {
        public TObject Create<TObject>(string name) where TObject : Object;
    }
}