using System;
using Service;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float Acceleration { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }

        [field: Header("GroundDetection")]
        [field: SerializeField]
        public float FeetRadius { get; private set; }

        [field: SerializeField] public LayerMask FeetMask { get; private set; }

        public Rigidbody2D Rigidbody { get; private set; }
        public CircleCollider2D Collider { get; private set; }

        public Vector2 FeetPosition => (Vector2)transform.position + Collider.offset + Vector2.down * Collider.radius;
        public bool IsGrounded { get; private set; }

        private IInputService _inputService;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();

            _inputService = Services.Instance.Single<IInputService>();
        }

        private void FixedUpdate()
        {
            IsGrounded = Physics2D.OverlapCircle(FeetPosition, FeetRadius, FeetMask);

            var velocity = Rigidbody.linearVelocity;

            var targetHorizontalVelocity = Mathf.MoveTowards(velocity.x, MaxSpeed * _inputService.Horizontal,
                Acceleration * Time.fixedDeltaTime);

            velocity.x = targetHorizontalVelocity;

            Rigidbody.linearVelocity = velocity;
        }

        private void Update()
        {
            if (_inputService.IsJump) TryJump();
        }

        private void TryJump()
        {
            if (IsGrounded && Rigidbody.linearVelocityY <= 0) Rigidbody.linearVelocityY = JumpForce;
        }
    }
}