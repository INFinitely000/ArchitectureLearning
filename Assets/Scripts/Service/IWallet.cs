using System;

namespace Service
{
    public interface IWallet : IService
    {
        public int Coins { get; }
        public int MaxCoins { get; }

        public event Action<int> Changed;
        
        public bool TryPut(int count);
        public bool TryTake(int count);
        public bool Has(int count);
    }
}