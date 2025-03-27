using UnityEngine;

namespace Service
{
    public class StandaloneInputService : IInputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const string JumpButton = "Jump";
        private const string FireButton = "Fire1";
        private const string GrabButton = "Fire2";
        private const string SprintButton = "Fire3";

        public float Horizontal => Input.GetAxisRaw(HorizontalAxis);
        public float Vertical => Input.GetAxisRaw(VerticalAxis);
        public bool IsJump => Input.GetButton(JumpButton);
        public bool IsFire => Input.GetButtonDown(FireButton);
        public bool IsGrab => Input.GetButton(GrabButton);
        public bool IsSprint => Input.GetButton(SprintButton);
    }
}