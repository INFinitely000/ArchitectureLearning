using System;

namespace Service
{
    public class Wallet : IWallet
    {
        public int Coins { get; private set; }
        public int MaxCoins { get; private set; }
        
        public event Action<int> Changed;


        public Wallet(int startCoins, int maxCoins)
        {
            Coins = startCoins;
            MaxCoins = maxCoins;
        }
        
        public bool TryPut(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

            if (MaxCoins - Coins < count) return false;

            Coins += count;

            Changed?.Invoke(count);

            return true;
        }

        public bool TryTake(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

            if (Coins < count) return false;

            Coins -= count;
            
            Changed?.Invoke(-count);

            return true;
        }

        public bool Has(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

            return Coins >= count;
        }
    }
}