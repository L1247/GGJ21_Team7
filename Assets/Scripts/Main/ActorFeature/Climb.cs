using Main.Input;
using UnityEngine;

namespace Main.ActorFeature
{
    public class Climb : MonoBehaviour
    {
    #region Private Variables

        private Actor         _actor;
        private IInputService _inputService;

        [SerializeField]
        private float JumpForce;

        [SerializeField]
        private KeyCode climbDownKeyCode = KeyCode.DownArrow;

        [SerializeField]
        private KeyCode climbUpKeyCode = KeyCode.UpArrow;

    #endregion

    #region Unity events

        private void Start()
        {
            _inputService.RegisterKey(climbUpKeyCode);
            _inputService.RegisterKey(climbDownKeyCode);
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
            var isOnLadder = _actor.IsOnLadder();
            if (isOnLadder)
            {
                // if (_inputService.IsKeyDown(climbUpKeyCode))
                // _actor.Climb(JumpForce);
                // if (_inputService.IsKeyDown(climbUpKeyCode))
            }
        }

    #endregion
    }
}