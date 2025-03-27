using System;
using Service;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.MainPlayer
{
    public class PlayerBombThrower : MonoBehaviour
    {
        [field: SerializeField] public float Force { get; private set; }
        [field: SerializeField] public float TimeBetweenThrows { get; private set; }

        private IGameFactory _factory;
        private float _lastThrowTime;

        public Vector3 velocity;
        
        
        private void Awake()
        {
            _factory = Services.Instance.Single<IGameFactory>();
        }

        public bool TryThrow()
        {
            if (Time.time - _lastThrowTime < TimeBetweenThrows) return false;

            _factory.CreateBomb(transform.position, velocity * Force);

            return true;
        }
    }
}