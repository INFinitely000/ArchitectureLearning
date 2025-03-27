using System;
using UnityEngine;

namespace Gameplay.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Projectile : MonoBehaviour
    {
        public Rigidbody2D Rigidbody { get; private set; }
        public CircleCollider2D Collider { get; private set; }

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();
        }
    }
}