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

        private Vector3 _previousPosition;


        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();
        }
        
        private void Start()
        {
            _previousPosition = transform.position;
        }

        private void FixedUpdate()
        {
            var velocity = Vector3.right * Speed * horizontal * Time.fixedDeltaTime;
                velocity.y = (transform.position.y - _previousPosition.y) * Time.fixedDeltaTime + Physics2D.gravity.y * Time.fixedDeltaTime * Rigidbody.gravityScale;

            Rigidbody.MovePosition(transform.position + velocity);

            _previousPosition = transform.position;
        }
    }
}