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
        
        [field: Header("Legs")]
        [field: SerializeField] public float LegLength { get; private set; }
        [field: SerializeField] public float LegStrength { get; private set; }
        
        [field: Header("GroundDetection")]
        [field: SerializeField] public float FeetRadius { get; private set; }
        [field: SerializeField] public LayerMask FeetMask { get; private set; }

        public Rigidbody2D Rigidbody { get; private set; }
        public CircleCollider2D Collider { get; private set; }

        public Vector2 FeetPosition => (Vector2)transform.position + Collider.offset + Vector2.down * (Collider.radius + LegLength);
        public bool IsGrounded { get; private set; }

        private IInputService _inputService;
        private RaycastHit2D _hit;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();

            _inputService = Services.Instance.Single<IInputService>();
        }

        private void FixedUpdate()
        {
            _hit = Physics2D.CircleCast(transform.position, FeetRadius, Vector2.down, Vector3.Distance(transform.position, FeetPosition), FeetMask);

            IsGrounded = _hit.transform;

            var currentLegLength = IsGrounded ? transform.position.y - _hit.point.y : LegLength;
            
            var velocity = Rigidbody.linearVelocity;

            var targetHorizontalVelocity = Mathf.MoveTowards(velocity.x, MaxSpeed * _inputService.Horizontal,
                Acceleration * Time.fixedDeltaTime);

            var targetVerticalVelocity = IsGrounded ? (LegLength - currentLegLength) * LegStrength : velocity.y;
            targetVerticalVelocity = Mathf.Max(targetVerticalVelocity, velocity.y);    
            
            velocity.x = targetHorizontalVelocity;
            velocity.y = targetVerticalVelocity;
            
            Rigidbody.linearVelocity = velocity;
        }

        private void Update()
        {
            if (_inputService.IsJump) TryJump();
        }

        private void TryJump()
        {
            if (IsGrounded) Rigidbody.linearVelocityY = JumpForce;
        }

        private void OnDrawGizmos()
        {
            if (_hit.transform == false) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _hit.point);
        }
    }
}