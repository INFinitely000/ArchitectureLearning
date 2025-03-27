using System;
using UnityEngine;

public class DelayedComponent : MonoBehaviour
{
    [field: SerializeField] public Behaviour Behaviour { get; private set; }
    [field: SerializeField] public float Delay { get; private set; }


    private void Awake()
    {
        Behaviour.enabled = false;
        
        Invoke(nameof(EnableComponent), Delay);
    }

    private void EnableComponent()
    {
        Behaviour.enabled = true;
    }
}
