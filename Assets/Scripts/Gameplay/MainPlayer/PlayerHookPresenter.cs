using System;
using UnityEngine;

namespace Gameplay.MainPlayer
{
    public class PlayerHookPresenter : MonoBehaviour
    {
        [field: SerializeField] public LineRenderer Renderer { get; private set; }
        [field: SerializeField] public PlayerHook Hook { get; private set; }
        
        private void Update()
        {
            Renderer.enabled = Hook.IsGrabbed;

            if (Hook.IsGrabbed == false) return;
            
            var from = transform.position;
            var to = Hook.Point.Rigidbody.position;

            Renderer.positionCount = 2;
            
            Renderer.SetPosition(0, from);
            Renderer.SetPosition(1, to);
        }
    }
}