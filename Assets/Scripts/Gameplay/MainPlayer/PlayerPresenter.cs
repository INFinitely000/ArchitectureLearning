using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.MainPlayer
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

        private Coroutine _damageAnimationRoutine;


        private void Update()
        {
            Animator.SetFloat(HorizontalSpeedHash, horizontalSpeed);
            Animator.SetBool(IsGroundedHash, isGrounded);

            Renderer.flipX = isFlip;
        }
        
        public void OnTakedDamage()
        {
            if (_damageAnimationRoutine != null) StopCoroutine(_damageAnimationRoutine);
            
            _damageAnimationRoutine = StartCoroutine(PlayDamageAnimation());
        }

        private IEnumerator PlayDamageAnimation()
        {
            var time = 0f;

            while (time < 1f)
            {
                Renderer.color = Color.red;
                
                time += Time.deltaTime * 5f;

                yield return null;
            }

            Renderer.color = Color.white;
        }
    }
}