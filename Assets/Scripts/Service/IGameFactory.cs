using Gameplay.MainPlayer;

namespace Service
{
    public interface IGameFactory : IFactory
    {
        Player Player { get; }
        PlayerCamera PlayerCamera { get; }
        Player CreatePlayer();
        PlayerCamera CreatePlayerCamera();
    }
}