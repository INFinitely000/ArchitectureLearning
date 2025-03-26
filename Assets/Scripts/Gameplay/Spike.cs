using System;
using UnityEngine;

namespace Gameplay
{
    public class Spike : MonoBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(Damage);
            }
        }
    }
}