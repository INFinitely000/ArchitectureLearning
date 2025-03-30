using System;
using Gameplay.MainPlayer;
using Service;
using UnityEngine;

namespace Gameplay
{
    public class Coin : MonoBehaviour
    {
        [field: SerializeField] public int Cost { get; private set; }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent<Player>(out var player))
            {
                Services.Instance.Single<IWallet>().TryPut(Cost);
                
                InstantParticles.Play("Coin Collected", transform.position, Quaternion.identity);
                
                Destroy(gameObject);
            }
        }
    }
}