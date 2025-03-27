using Gameplay.MainPlayer;
using Gameplay.Projectiles;
using UnityEngine;

namespace Service
{
    public interface IGameFactory : IFactory
    {
        Player Player { get; }
        PlayerCamera PlayerCamera { get; }
        Player CreatePlayer();
        PlayerCamera CreatePlayerCamera();

        Bomb CreateBomb(Vector2 position, Vector2 velocity, float size = 1f);
    }
}