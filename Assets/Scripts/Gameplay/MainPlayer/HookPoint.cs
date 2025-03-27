using UnityEngine;

namespace Gameplay.MainPlayer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HookPoint : MonoBehaviour, IHookPoint
    {
        public Rigidbody2D Rigidbody { get; private set; }


        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}