using System;
using UnityEngine;

public class TemponaryObject : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject);
    }
}
