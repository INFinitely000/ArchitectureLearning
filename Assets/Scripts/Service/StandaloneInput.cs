using UnityEngine;

namespace Service
{
    public class StandaloneInput : IInput
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const string JumpButton = "Jump";
        private const string FireButton = "Fire1";
        private const string SprintButton = "Fire3";
        private const string RestartButton = "Restart";

        public float Horizontal => Input.GetAxisRaw(HorizontalAxis);
        public float Vertical => Input.GetAxisRaw(VerticalAxis);
        public bool IsJump => Input.GetButton(JumpButton);
        public bool IsFire => Input.GetButtonDown(FireButton);
        public bool IsSprint => Input.GetButton(SprintButton);
        public bool IsRestart => Input.GetButtonDown(RestartButton);
    }
}