using System;
using System.Collections.Generic;

namespace Service
{
    public class Services
    {
        private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public static Services Instance { get; private set; }

        public Services() => Instance = this;
        
        public void Register<TService>(TService service) where TService : IService
        {
            if (_services.ContainsKey(typeof(TService))) return;

            _services.Add(typeof(TService), service);
        }

        public void Unregister<TService>() where TService : IService
        {
            if (_services.ContainsKey(typeof(TService)) == false) return;

            _services.Remove(typeof(TService));
        }

        public TService Single<TService>() where TService : IService
        {
            return (TService)_services.GetValueOrDefault(typeof(TService));
        }
    }
}
