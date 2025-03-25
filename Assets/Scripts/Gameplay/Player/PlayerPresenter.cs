using System;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerPresenter : MonoBehaviour, IPlayerComponent
    {
        public static readonly int HorizontalSpeedHash = UnityEngine.Animator.StringToHash("horizontalSpeed");
        public static readonly int IsGroundedHash = UnityEngine.Animator.StringToHash("isGrounded");
        
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }
        
        public Player Player { get; private set; }

        [HideInInspector, NonSerialized] public float horizontalSpeed;
        [HideInInspector, NonSerialized] public bool isGrounded;
        [HideInInspector, NonSerialized] public bool isFlip;
        
        public void SetPlayer(Player player) => Player = player;


        private void Update()
        {
            Animator.SetFloat(HorizontalSpeedHash, horizontalSpeed);
            Animator.SetBool(IsGroundedHash, isGrounded);

            Renderer.flipX = isFlip;
        }
    }
}