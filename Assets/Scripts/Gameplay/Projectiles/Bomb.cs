using System;
using System.Collections;
using System.Linq;
using Service;
using UnityEngine;

namespace Gameplay.Projectiles
{
    public class Bomb : Projectile
    {
        private const float ConcatFactor = 0.125f;
        private const float MaxSize = 1.75f;
        
        [field: SerializeField, Min(0f)] public float Duration { get; private set; }
        [field: SerializeField, Min(0f)] public float ExplodeRadius { get; private set; }
        [field: SerializeField, Min(0f)] public float ExplodeForce { get; private set; }
        [field: SerializeField, Min(0f)] public int ExplodeDamage { get; private set; }

        public float Size { get; private set; } = 1f;

        public static event Action<Bomb> Exploded;

        private float _fuse;
        
        
        private void Start()
        {
            _fuse = Duration;
        }

        public void SetSize(float size)
        {
            transform.localScale = Vector3.one * size;

            ExplodeDamage *= (int)size;
            ExplodeRadius *= size;
            ExplodeForce *= size;

            Size = size;
        }

        private void FixedUpdate()
        {
            _fuse = Mathf.MoveTowards(_fuse, 0, Time.fixedDeltaTime);

            if (_fuse == 0) Explode();
        }

        private void Explode()
        {
            var hits = Physics2D.CircleCastAll(transform.position, ExplodeRadius, Vector3.down, ExplodeRadius);

            foreach (var hit in hits)
            { 
                var multiplier = Mathf.Max((ExplodeRadius - Vector3.Distance(hit.transform.position, transform.position)) / ExplodeRadius, 0f);
                if (hit.transform.TryGetComponent<Rigidbody2D>(out var rigidbody))
                {
                    var direction = (hit.transform.position - transform.position).normalized;
                    
                    rigidbody.AddForce(direction * ExplodeForce * multiplier);
                }
                
                if (hit.transform.TryGetComponent<IEnemyDamageable>(out var damageable))
                    damageable.TakeDamage(Mathf.RoundToInt(ExplodeDamage * multiplier));
            }   

            Exploded?.Invoke(this);
            
            InstantParticles.Play("Bomb Explode", transform.position, 0f);
            
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (enabled == false) return;
            
            if (other.transform.TryGetComponent<Bomb>(out var anotherBomb))
            {
                var minSize = Mathf.Min(Size, anotherBomb.Size);
                var maxSize = Mathf.Max(Size, anotherBomb.Size);
                
                var size = Mathf.Min(minSize * ConcatFactor + maxSize, MaxSize);
                var position = anotherBomb.Size >= Size ? anotherBomb.transform.position : transform.position;
                var velocity = (anotherBomb.Size >= Size ? anotherBomb.Rigidbody.linearVelocity : Rigidbody.linearVelocity) + Vector2.up;

                Destroy(gameObject);
                Destroy(anotherBomb.gameObject);

                anotherBomb.enabled = false;
                
                Services.Instance.Single<IGameFactory>().CreateBomb(position, velocity, size);
            }
            else if (other.transform.TryGetComponent<IEnemyDamageable>(out var enemyDamageable))
            {
                Explode();
            }
        }
    }
}