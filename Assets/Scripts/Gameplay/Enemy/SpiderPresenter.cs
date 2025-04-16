using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class SpiderPresenter : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }

        [HideInInspector, NonSerialized] public bool isFlip;

        private Coroutine _damageAnimationRoutine;
        

        private void Update()
        {
            Renderer.flipX = isFlip;
        }


        public void OnTakedDamage()
        {
            if (_damageAnimationRoutine != null) StopCoroutine(_damageAnimationRoutine);
            
            _damageAnimationRoutine = StartCoroutine(PlayDamageAnimation());
        }

        private IEnumerator PlayDamageAnimation()
        {
            var time = 0f;

            while (time < 1f)
            {
                Renderer.color = Color.red;
                
                time += Time.deltaTime * 5f;

                yield return null;
            }

            Renderer.color = Color.white;
        }
    }
}