using UnityEngine;

namespace Main.ActorFeature
{
    public class MoveLeft : Feature
    {
    #region Private Variables

        [SerializeField]
        private float moveSpeed = 5;

        [SerializeField]
        private KeyCode LeftMoveKeyCode = KeyCode.LeftArrow;

    #endregion

    #region Unity events

        private void Start()
        {
            _inputService.RegisterKey(LeftMoveKeyCode);
        }

    #endregion

    #region Private Methods

        private void Update()
        {
            if (_inputService.IsKeyPress(LeftMoveKeyCode))
                _actor.AddMovementX(-1 * moveSpeed);
        }

    #endregion
    }
}