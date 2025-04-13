using System;
using Gameplay.MainPlayer;
using Service;
using UnityEngine;

namespace Gameplay.UI
{
    public class GameUI : MonoBehaviour
    {
        [field: SerializeField] public HealthUI PlayerHealth { get; private set; }
    }
}