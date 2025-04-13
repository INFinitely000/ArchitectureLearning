using System;
using UnityEngine;

namespace Gameplay
{
    public class GameloopProvider : MonoBehaviour
    {
        private event Action _updateCallback;
        
        public void Construct(Action updateCallback)
        {
            _updateCallback = updateCallback;
        }

        private void Update()
        {
            _updateCallback?.Invoke();
        }
    }
}