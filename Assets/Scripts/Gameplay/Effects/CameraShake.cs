using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.Effects
{
    public class CameraShake : MonoBehaviour
    {
        private Coroutine _routine;


        public void Shake(float intensity, float duration)
        {
            if (_routine != null) StopCoroutine(_routine);

            StartCoroutine(_Shake(intensity, duration));
        }
        
        
        private IEnumerator _Shake(float intensity, float duration)
        {
            var time = 0f;

            while (time < duration)
            {
                transform.position += (Vector3)Vector2.one * Mathf.Sin(time / duration * Mathf.PI * 2) * intensity;
                
                time += Time.deltaTime;
                
                yield return null;
            }
        }
    }
}