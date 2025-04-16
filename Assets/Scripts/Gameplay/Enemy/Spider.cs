using System;
using Gameplay.MainPlayer;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class Spider : Enemy
    {
        [field: SerializeField] public SpiderMovement Movement { get; private set; }
        [field: SerializeField] public SpiderPresenter Presenter { get; private set; }
        [field: SerializeField] public SpiderHealth Health { get; private set; }
        [field: Header("Ground Area")]
        [field: SerializeField] public Vector3 GroundAreaOffset { get; private set; }
        [field: SerializeField] public float GroundAreaRadius { get; private set; }
        [field: Header("Wall Area")]
        [field: SerializeField] public Vector3 WallAreaOffset { get; private set; }
        [field: SerializeField] public float WallAreaRadius { get; private set; }
        [field: Space]
        [field: SerializeField] public LayerMask Mask { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }

        public bool direction { get; private set; }

        private void Awake() => 
            Movement.horizontal = direction ? 1 : -1;

        private void Update() => 
            Presenter.isFlip = direction;

        private void OnEnable() =>
            Health.Changed += OnHealthChanged;

        private void OnDisable() =>
            Health.Changed -= OnHealthChanged;
        
        private void FixedUpdate()
        {
            var groundAreaOffset = GroundAreaOffset;
                groundAreaOffset.x *= direction ? 1 : -1;

            var wallAreaOffset = WallAreaOffset;
                wallAreaOffset.x *= direction ? 1 : -1;    
            
            var isHasGround = Physics2D.OverlapCircle(transform.position + groundAreaOffset, GroundAreaRadius, Mask);
            var isHasWall = Physics2D.OverlapCircle(transform.position + wallAreaOffset, WallAreaRadius, Mask);

            if (isHasGround == false || isHasWall)
            {
                direction = !direction;
                
                Movement.horizontal = direction ? 1 : -1;
            }
        }

        private void OnHealthChanged(int difference)
        {
            if (difference >= 0) return;
            
            Presenter.OnTakedDamage();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<IPlayerDamageable>(out var damageable))
            {
                damageable.TakeDamage(Damage);
                
                direction = !direction;
                
                Movement.horizontal = direction ? 1 : -1;
            }
        }

        private void OnDrawGizmos()
        {
            var groundAreaOffset = GroundAreaOffset;
                groundAreaOffset.x *= direction ? 1 : -1;

            var wallAreaOffset = WallAreaOffset;
                wallAreaOffset.x *= direction ? 1 : -1;
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position + groundAreaOffset, GroundAreaRadius);
            
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + wallAreaOffset, WallAreaRadius);
        }
    }
}