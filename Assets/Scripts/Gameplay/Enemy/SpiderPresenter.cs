using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class SpiderPresenter : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }

        [HideInInspector, NonSerialized] public bool isFlip;
        

        private void Update()
        {
            Renderer.flipX = isFlip;
        }
    }
}