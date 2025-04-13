using System;
using UnityEngine;

namespace Gameplay
{
    public class Breakable: MonoBehaviour, IEnemyDamageable, IHealthable
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public string ParticleName { get; private set; }

        public int Health { get; private set; }
        public event Action<int> Changed;

        private void Awake()
        {
            Health = MaxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            if (damage < 0) throw new ArgumentOutOfRangeException(nameof(damage));

            if (Health < 1) return;
            
            Health -= Mathf.Min(damage, Health);

            Changed?.Invoke(damage);
            
            InstantParticles.Play(ParticleName, transform.position, 0f);
            
            if (Health < 1)
                Kill();
        }

        public void Kill()
        {
            Destroy(gameObject);
        }
    }
}