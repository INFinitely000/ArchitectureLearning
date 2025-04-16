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
        
        [field: Header("Water Physics")]
        [field: SerializeField] public float SurfaceForce { get; private set; }
        [field: SerializeField] public float DiveForce { get; private set; }

        public Rigidbody2D Rigidbody { get; private set; }
        public BoxCollider2D Collider { get; private set; }

        public Vector2 FeetPosition => (Vector2)transform.position + (Collider.offset + Vector2.down * (Collider.size.y + Collider.edgeRadius)) * transform.localScale.y;
        public bool IsGrounded { get; private set; }
        public bool IsSwimming { get; private set; }

        private IInput _input;
        private RaycastHit2D _hit;

        [HideInInspector, NonSerialized] public float horizontal;
        [HideInInspector, NonSerialized] public bool isSprint;
        [HideInInspector, NonSerialized] public bool isJump;
        [HideInInspector, NonSerialized] public bool isDown;
        
        
        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();
        }

        protected virtual void FixedUpdate()
        {
            _hit = Physics2D.CircleCast(FeetPosition, FeetRadius, Vector2.down, FeetRadius, FeetMask);

            IsGrounded = _hit.transform != null;
            IsSwimming = _hit.transform?.GetComponent<Water>() != null;
            
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
            if (isDown) TryDown();
        }

        protected virtual void TryJump()
        {
            if (IsGrounded) Rigidbody.linearVelocityY = IsSwimming ? SurfaceForce : JumpForce;
        }

        protected virtual void TryDown()
        {
            Rigidbody.linearVelocityY = IsSwimming ? -DiveForce : -JumpForce;
        }
    }
}