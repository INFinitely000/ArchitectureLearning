using Gameplay.MainPlayer;
using Service;

namespace Gameplay
{
    public class CameraBackgroundParallax : Parallax
    {
        private void Awake()
        {
            var camera = Services.Instance.Single<IGameFactory>().PlayerCamera;
            
            SetTarget(camera.transform);
        }
    }
}