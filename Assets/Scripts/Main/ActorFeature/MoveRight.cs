using Main.Input;
using UnityEngine;

namespace Main.ActorFeature
{
    public class MoveRight : Feature
    {
    #region Private Variables
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

        private void Update()
        {
            if (_inputService.IsKeyPress(RightMoveKeyCode))
                _actor.AddMovementX(moveSpeed);
        }

    #endregion
    }
}