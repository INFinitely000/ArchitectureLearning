using UnityEngine;

namespace Gameplay
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public GameplayType StartTypeOfGameplay { get; private set; }
    }
}