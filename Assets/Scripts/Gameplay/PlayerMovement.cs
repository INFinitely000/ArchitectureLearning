using System;
using Service;
using UnityEngine;

namespace Gameplay.MainPlayer
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float MaxSprintSpeed { get; private set; }
        [field: SerializeField] public float Acceleration { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        
        [field: Header("Ground Detection")]
        [field: SerializeField] public float FeetRadius { get; private set; }
        [field: SerializeField] public LayerMask FeetMask { get; private set; }

        public Rigidbody2D Rigidbody { get; private set; }
        public BoxCollider2D Collider { get; private set; }

        public Vector2 FeetPosition => (Vector2)transform.position + (Collider.offset + Vector2.down * (Collider.size.y + Collider.edgeRadius)) * transform.localScale.y;
        public bool IsGrounded { get; private set; }

        private IInput _input;
        private RaycastHit2D _hit;

        [HideInInspector, NonSerialized] public float horizontal;
        [HideInInspector, NonSerialized] public bool isSprint;
        [HideInInspector, NonSerialized] public bool isJump;
        

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();
        }

        protected virtual void FixedUpdate()
        {
            IsGrounded = Physics2D.OverlapCircle(FeetPosition, FeetRadius, FeetMask);

            var velocity = Rigidbody.linearVelocity;

            var speed = isSprint ? MaxSprintSpeed : MaxSpeed;
            
            var targetHorizontalVelocity = Mathf.MoveTowards(velocity.x, speed * horizontal,
                Acceleration * Time.fixedDeltaTime);

            velocity.x = targetHorizontalVelocity;
            
            Rigidbody.linearVelocity = velocity;
        }

        protected virtual void Update()
        {
            if (isJump) TryJump();
        }

        protected virtual void TryJump()
        {
            if (IsGrounded) Rigidbody.linearVelocityY = JumpForce;
        }
    }
}