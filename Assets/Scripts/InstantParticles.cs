using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstantParticles : MonoBehaviour
{
    private static Dictionary<string, ParticleSystem> _particles;


    private void Awake()
    {
        _particles = GetComponentsInChildren<ParticleSystem>().ToDictionary(p => p.name);
    }


    public static void Play(string name, Vector3 position, Quaternion rotation)
    {
        if (_particles.TryGetValue(name, out var particle))
        {
            particle.transform.position = position;
            particle.transform.rotation = rotation;
            
            particle.Play(true);
        }
        else
        {
            Debug.LogWarning("Can't find particles with name: " + name);
        }
    }
}
