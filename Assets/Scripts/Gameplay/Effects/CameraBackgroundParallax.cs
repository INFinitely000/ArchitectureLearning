using Gameplay.MainPlayer;
using Service;
using UnityEngine;

namespace Gameplay
{
    public class CameraBackgroundParallax : Parallax
    {
        public override Transform Target => Camera.main.transform;
    }
}