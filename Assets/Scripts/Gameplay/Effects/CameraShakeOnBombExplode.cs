using System;
using Gameplay.Projectiles;
using UnityEngine;

namespace Gameplay.Effects
{
    public class CameraShakeOnBombExplode : MonoBehaviour
    {
        [field: SerializeField] public CameraShake CameraShake { get; private set; }
        [field: SerializeField] public float MaxDistance { get; private set; }
        [field: SerializeField] public float Intensity { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        
        
        private void OnEnable()
        {
            Bomb.Exploded += OnBombExploded;
        }

        private void OnDisable()
        {
            Bomb.Exploded -= OnBombExploded;
        }

        private void OnBombExploded(Bomb bomb)
        {
            var distance = Vector2.Distance( Camera.main.transform.position, bomb.transform.position );

            if (distance > MaxDistance) return;

            var intensity = Intensity * (MaxDistance - distance) / MaxDistance;
            
            CameraShake.Shake(intensity, Duration);
        }
    }
}