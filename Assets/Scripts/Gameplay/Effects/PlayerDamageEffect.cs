using System;
using UnityEngine;

namespace Gameplay.MainPlayer
{
    public class PlayerDamageEffect : MonoBehaviour
    {
        [field: SerializeField] public Player Player { get; private set; }


        private void OnEnable()
        {
            Player.Health.Changed += OnHealthChanged;
        }

        private void OnDisable()
        {
            Player.Health.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(int difference)
        {
            if (difference > 0) return;

            InstantParticles.Play("Player Damage", Player.transform.position, 0f);
        }
    }
}