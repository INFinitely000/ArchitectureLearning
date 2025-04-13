using System;
using RuntimeConsole;
using Service;
using UnityEngine;

namespace Gameplay.MainPlayer
{
    public class PlayerHealth : MonoBehaviour, IHealthable, IPlayerDamageable, IPlayerComponent
    {
        [field: SerializeField] public float MaxTimeBetweenDamages { get; private set; }
        
        public Player Player { get; private set; }

        public int MaxHealth { get; private set; }
        
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
        

        public void SetPlayer(Player player) => Player = player;

        public void SetMaxHealth(int maxHealth) => MaxHealth = Mathf.Max(maxHealth, 1);
        
        public void SetHealth(int health) => Health = Mathf.Max(health, 1);

        public void TakeDamage(int damage)
        {
            if (damage < 0) throw new ArgumentOutOfRangeException(nameof(damage));

            if (Health < 1) return;

            if (Time.time - _lastDamageTime < MaxTimeBetweenDamages)
                return;
            
            Health -= Mathf.Min(damage, Health);
            _lastDamageTime = Time.time;
            
            Changed?.Invoke(damage);
            
            if (Health < 1)
                Kill();
        }

        public void Kill()
        {
            Destroy(Player.gameObject);
        }
    }
}