using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class InstantParticles : MonoBehaviour
{
    private static Dictionary<string, ParticleSystem> _particles;


    private void Awake()
    {
        _particles = GetComponentsInChildren<ParticleSystem>().ToDictionary(p => p.name);
    }


    public static void Play(string name, Vector3 position, float rotation)
    {
        if (_particles.TryGetValue(name, out var particle))
        {
            var shape = particle.shape;
            
            shape.position = position;
            shape.rotation = Vector3.forward * rotation;
            
            particle.Play(true);
        }
        else
        {
            Debug.LogWarning("Can't find particles with name: " + name);
        }
    }
}
