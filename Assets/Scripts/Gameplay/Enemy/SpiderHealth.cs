using System;
using RuntimeConsole;
using Service;
using UnityEngine;

namespace Gameplay.MainPlayer
{
    public class SpiderHealth : MonoBehaviour, IHealthable, IEnemyDamageable
    {
        [field: SerializeField] public float MaxTimeBetweenDamages { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; }
        
        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                var difference = value - _health;
                
                _health = value;

                if (difference != 0)
                    Changed?.Invoke(difference);
            }
        }

        public event Action<int> Changed;

        private int _health;
        private float _lastDamageTime;
        
        public void SetMaxHealth(int maxHealth) => MaxHealth = Mathf.Max(maxHealth, 1);
        
        public void SetHealth(int health) => Health = Mathf.Max(health, 1);

        private void Start()
        {
            _health = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0) throw new ArgumentOutOfRangeException(nameof(damage));

            if (Health < 1) return;

            if (Time.time - _lastDamageTime < MaxTimeBetweenDamages)
                return;
            
            Health -= damage;
            _lastDamageTime = Time.time;
            
            Changed?.Invoke(damage);
            
            if (Health < 1)
                Kill();
        }

        public void Kill()
        {
            Destroy(gameObject);
        }
    }
}