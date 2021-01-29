using Main.Input;
using UnityEngine;

namespace Main.ActorFeature
{
    public class MoveRight : MonoBehaviour
    {
    #region Private Variables

        private Actor         _actor;
        private IInputService _inputService;

        [SerializeField]
        private float moveSpeed = 5;

        [SerializeField]
        private KeyCode RightMoveKeyCode = KeyCode.RightArrow;

    #endregion

    #region Unity events

        private void Start()
        {
            _inputService.RegisterKey(RightMoveKeyCode);
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
            if (_inputService.IsKeyPress(RightMoveKeyCode))
                _actor.AddMovementX(moveSpeed);
        }

    #endregion
    }
}