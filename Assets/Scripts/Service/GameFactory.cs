using Data;
using Gameplay.MainPlayer;
using Gameplay.Projectiles;
using Gameplay.UI;
using UnityEngine;

namespace Service
{
    public class GameFactory : Factory, IGameFactory
    {
        private const string PlayerAssetName = "Player";
        private const string PlayerCameraAssetName = "PlayerCamera";
        private const string UIAssetName = "GameUI";
        private const string BombAssetName = "Bomb";

        public GameUI UI { get; private set; }
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

        public GameUI CreateUI()
        {
            UI = Create<GameUI>(UIAssetName);

            return UI;
        }

        public Bomb CreateBomb(Vector2 position, Vector2 velocity, float size = 1f)
        {
            var bomb = Create<Bomb>(BombAssetName);

            bomb.transform.position = position;
            bomb.Rigidbody.linearVelocity = velocity;
            
            bomb.SetSize(size);
            
            return bomb;
        }
    }
}