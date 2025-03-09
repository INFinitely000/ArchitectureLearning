using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Service
{
    public class SceneLoader : ISceneLoader
    {
        public readonly ICoroutineHandler coroutineHandler;

        private Coroutine _loadRoutine;

        public SceneLoader(ICoroutineHandler coroutineHandler)
        {
            this.coroutineHandler = coroutineHandler;
        }

        public void Load(string name, Action callback = null)
        {
            if (_loadRoutine != null) coroutineHandler.StopCoroutine(_loadRoutine);

            _loadRoutine = coroutineHandler.StartCoroutine(LoadAsync(name, callback));
        }

        private IEnumerator LoadAsync(string name, Action callback = null)
        {
            if (name == SceneManager.GetActiveScene().name)
            {
                callback?.Invoke();
                yield break;
            }

            var asyncOperation = SceneManager.LoadSceneAsync(name);
            yield return new WaitUntil(() => asyncOperation.isDone);

            callback?.Invoke();
        }
    }
}