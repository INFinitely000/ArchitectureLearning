using Service;

namespace Data
{
    public interface IGameData : IService
    {
        public PlayerData player { get; }
    }
}