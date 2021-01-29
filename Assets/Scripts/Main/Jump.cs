using UnityEngine;

namespace Main
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
            if (_inputService.IsKeyDown(jumpKeyCode)) _actor.Jump(JumpForce);
        }

    #endregion
    }
}