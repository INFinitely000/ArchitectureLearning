using System;
using Service;
using UnityEngine;

namespace Gameplay.MainPlayer
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public PlayerMovement Movement { get; private set; }
        [field: SerializeField] public PlayerHealth Health { get; private set; }
        [field: SerializeField] public PlayerPresenter Presenter { get; private set; }
        [field: SerializeField] public PlayerBombThrower BombThrower { get; private set; }

        private IInput _input;


        public void Construct(IInput input)
        {
            _input = input;
        }

        private void Awake()
        {
            Health.SetPlayer(this);
            Presenter.SetPlayer(this);

        }
        
        private void FixedUpdate()
        {
            Presenter.horizontalSpeed = Mathf.Abs(Movement.Rigidbody.linearVelocityX);
            Presenter.isGrounded = Movement.IsGrounded;
            Presenter.isFlip = Movement.Rigidbody.linearVelocityX < 0;
        }

        private void Update()
        {
            Movement.horizontal = _input.Horizontal;
            Movement.isSprint = _input.IsSprint;
            Movement.isJump = _input.IsJump;
            BombThrower.velocity = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition) - transform.position;

            if (_input.IsFire)
                BombThrower.TryThrow();
        }
    }
}
