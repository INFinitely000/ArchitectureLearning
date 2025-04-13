using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AxeTrap : MonoBehaviour
    {
        [field: SerializeField] public float Rate { get; private set; }
        [field: SerializeField] public float MaxAngle { get; private set; }
        
        public Rigidbody2D Rigidbody { get; private set; }


        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(int.MaxValue);
            }
        }


        private void Update()
        {
            Rigidbody.MoveRotation( Mathf.Sin(Time.time * Rate * Mathf.PI) * MaxAngle );
        }
    }
}