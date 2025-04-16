namespace Service
{
    public interface IInput : IService
    {
        public float Horizontal { get; }
        public bool IsJump { get; }
        public bool IsDown { get; }
        public bool IsFire { get; }
        public bool IsSprint { get; }
        public bool IsRestart { get; }
    }
}
