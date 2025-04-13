using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Saw : MonoBehaviour
    {
        [field: SerializeField] public float Rate { get; private set; }
        [field: SerializeField, Min(1)] public int Damage { get; private set; }
        
        public Rigidbody2D Rigidbody { get; private set; }


        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(Damage);
            }
        }


        private void Update()
        {
            Rigidbody.MoveRotation( Time.time * Rate % 360 );
        }
    }
}