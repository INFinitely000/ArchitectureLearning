using Data;
using Gameplay.MainPlayer;

namespace Service
{
    public class GameFactory : Factory, IGameFactory
    {
        private const string PlayerAssetName = "Player";
        private const string PlayerCameraAssetName = "PlayerCamera";

        public Player Player { get; private set; }
        public PlayerCamera PlayerCamera { get; private set; }

        public GameFactory(AssetData assetData) : base(assetData)
        {
        }
        
        public Player CreatePlayer()
        {
            Player = Create<Player>(PlayerAssetName);

            return Player;
        }

        public PlayerCamera CreatePlayerCamera()
        {
            PlayerCamera = Create<PlayerCamera>(PlayerCameraAssetName);

            return PlayerCamera;
        }
    }
}