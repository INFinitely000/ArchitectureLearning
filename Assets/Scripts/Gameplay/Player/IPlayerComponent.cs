namespace Gameplay.Player
{
    public interface IPlayerComponent
    {
        public Player Player { get; }
        
        public void SetPlayer(Player player);
    }
}