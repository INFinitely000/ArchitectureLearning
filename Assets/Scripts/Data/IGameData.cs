using Service;

namespace Data
{
    public interface IGameData : IService
    {
        public PlayerData Player { get; }
        public WalletData Wallet { get; }
    }
}