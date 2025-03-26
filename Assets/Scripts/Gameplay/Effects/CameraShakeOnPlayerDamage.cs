using Gameplay.MainPlayer;
using Service;
using UnityEngine;

namespace Gameplay.Effects
{
    public class CameraShakeOnPlayerDamage : MonoBehaviour
    {
        [field: SerializeField] public CameraShake CameraShake { get; private set; }        
        [field: SerializeField] public float Invensity { get; private set; }        
        [field: SerializeField] public float Duration { get; private set; }        

        private void OnEnable()
        {
            Services.Instance.Single<IGameFactory>().Player.Health.Changed += OnHealthChanged;
        }

        private void OnDisable()
        {
            Services.Instance.Single<IGameFactory>().Player.Health.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(int difference)
        {
            CameraShake.Shake(Invensity, Duration);
        }
    }
}