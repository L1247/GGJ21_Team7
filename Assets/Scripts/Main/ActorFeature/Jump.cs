using Main.Input;
using UnityEngine;

namespace Main.ActorFeature
{
    public class Jump : Feature
    {
    #region Private Variables


        [SerializeField]
        private float JumpForce;

        [SerializeField]
        private KeyCode jumpKeyCode = KeyCode.Space;

    #endregion

    #region Unity events

        private void Start()
        {
            _inputService.RegisterKey(jumpKeyCode);
        }

    #endregion

    #region Private Methods



        private void Update()
        {
            var isGrounded    = _actor.IsGrounded();
            var isJumpKeyDown = _inputService.IsKeyDown(jumpKeyCode);
            if (isJumpKeyDown && isGrounded)
                _actor.Jump(JumpForce);
        }

    #endregion
    }
}