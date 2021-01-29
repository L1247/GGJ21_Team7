using Main.Input;
using UnityEngine;

namespace Main.ActorFeature
{
    public class Jump : MonoBehaviour
    {
    #region Private Variables

        private Actor         _actor;
        private IInputService _inputService;

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

        private void Awake()
        {
            _inputService = GetComponent<IInputService>();
            _actor        = GetComponent<Actor>();
        }

        private void Update()
        {
            var isGrounded    = _actor.IsGrounded();
            var isJumpKeyDown = _inputService.IsKeyDown(jumpKeyCode);
            Debug.Log($"{isGrounded} , {isJumpKeyDown}");
            if (isJumpKeyDown && isGrounded)
                _actor.Jump(JumpForce);
        }

    #endregion
    }
}