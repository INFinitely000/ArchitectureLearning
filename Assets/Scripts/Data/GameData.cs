using UnityEngine;

namespace Data
{
    [CreateAssetMenu( fileName = "GameData", menuName = "Data/GameData")]
    public class GameData : ScriptableObject, IGameData
    {
        [field: SerializeField] public PlayerData player { get; private set; }
    }
}