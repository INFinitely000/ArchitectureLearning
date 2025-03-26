using System;

namespace Gameplay
{
    public interface IHealthable : IDamageable
    {
        public int MaxHealth { get; }
        public int Health { get; }

        public event Action<int> Changed;
    }
}