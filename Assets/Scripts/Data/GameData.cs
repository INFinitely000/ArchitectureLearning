using UnityEngine;

namespace Data
{
    [CreateAssetMenu( fileName = "GameData", menuName = "Data/GameData")]
    public class GameData : ScriptableObject, IGameData
    {
        [field: SerializeField] public PlayerData Player { get; private set; }
        [field: SerializeField] public WalletData Wallet { get; private set; }
    }
}