using System;
using UnityEngine;

namespace Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public PlayerMovement Movement { get; private set; }
        [field: SerializeField] public PlayerPresenter Presenter { get; private set; }


        private void FixedUpdate()
        {
            Presenter.horizontalSpeed = Mathf.Abs(Movement.Rigidbody.linearVelocityX);
            Presenter.isGrounded = Movement.IsGrounded;
            Presenter.isFlip = Movement.Rigidbody.linearVelocityX < 0;
        }
    }
}
