namespace Service
{
    public interface IInputService : IService
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public bool IsJump { get; }
        public bool IsFire { get; }
        public bool IsSprint { get; }
    }
}
