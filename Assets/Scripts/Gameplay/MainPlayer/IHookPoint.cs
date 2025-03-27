using UnityEngine;

namespace Gameplay.MainPlayer
{
    public interface IHookPoint
    {
        public Rigidbody2D Rigidbody { get; }
    }
}