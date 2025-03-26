using System;
using Service;

namespace Gameplay.Player
{
    public class CameraBackgroundParallax : Parallax
    {
        private void Awake()
        {
            var player = Services.Instance.Single<IFactory>().GetCreatedObject<PlayerCamera>("PlayerCamera");
            
            SetTarget(player.transform);
        }
    }
}