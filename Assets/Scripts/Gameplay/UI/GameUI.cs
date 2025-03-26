using System;
using Gameplay.MainPlayer;
using Service;
using UnityEngine;

namespace Gameplay.UI
{
    public class GameUI : MonoBehaviour
    {
        [field: SerializeField] public HealthUI PlayerHealth { get; private set; }


        private void Awake()
        {
            var player = Services.Instance.Single<IGameFactory>().Player;
            var healthComponent = player.Health;
            
            PlayerHealth.SetHealth( healthComponent );
        }
    }
}