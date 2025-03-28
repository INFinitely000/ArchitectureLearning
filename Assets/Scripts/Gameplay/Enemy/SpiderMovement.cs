using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpiderMovement : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; private set; }
        
        public Rigidbody2D Rigidbody { get; private set; }
        public BoxCollider2D Collider { get; private set; }

        [HideInInspector, NonSerialized] public float horizontal;
        
        
        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            var velocity = Vector3.right * Speed * horizontal * Time.fixedDeltaTime;
                velocity.y = Rigidbody.linearVelocityY;

            Rigidbody.MovePosition(transform.position + velocity);
        }
    }
}