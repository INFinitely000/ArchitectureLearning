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

        private IInputService _inputService;
        

        private void Awake()
        {
            Health.SetPlayer(this);
            Presenter.SetPlayer(this);

            _inputService = Services.Instance.Single<IInputService>();
        }


        private void FixedUpdate()
        {
            Presenter.horizontalSpeed = Mathf.Abs(Movement.Rigidbody.linearVelocityX);
            Presenter.isGrounded = Movement.IsGrounded;
            Presenter.isFlip = Movement.Rigidbody.linearVelocityX < 0;
        }

        private void Update()
        {
            Movement.horizontal = _inputService.Horizontal;
            Movement.isSprint = _inputService.IsSprint;
            Movement.isJump = _inputService.IsJump;
            BombThrower.velocity = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition) - transform.position;

            if (_inputService.IsFire)
                BombThrower.TryThrow();
        }
    }
}
