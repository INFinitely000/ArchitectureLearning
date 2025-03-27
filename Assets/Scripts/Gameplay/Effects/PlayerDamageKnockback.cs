using System;
using UnityEngine;

namespace Gameplay.MainPlayer
{
    public class PlayerDamageKnockback : MonoBehaviour
    {
        [field: SerializeField] public Player Player { get; private set; }
        [field: SerializeField] public Vector2 ImpulsePower { get; private set; }


        private void Awake()
        {
            Player.Health.Changed += OnHealthChanged;
        }

        private void OnDestroy()
        {
            Player.Health.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(int difference)
        {
            if (difference > 0) return;
            
            Player.Movement.Rigidbody.linearVelocity = ImpulsePower;
        }
    }
}