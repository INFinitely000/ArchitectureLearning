using System.Collections;
using UnityEngine;

namespace Service
{
    public interface ICoroutineHandler
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(Coroutine coroutine);
    }
}