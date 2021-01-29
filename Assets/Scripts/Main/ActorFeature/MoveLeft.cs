using Main.Input;
using UnityEngine;

namespace Main.ActorFeature
{
    public class MoveLeft : MonoBehaviour
    {
    #region Private Variables

        private Actor         _actor;
        private IInputService _inputService;

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

        private void Awake()
        {
            _inputService = GetComponent<IInputService>();
            _actor        = GetComponent<Actor>();
        }

        private void Update()
        {
            if (_inputService.IsKeyPress(LeftMoveKeyCode))
                _actor.AddMovement(Vector2.left * moveSpeed);
        }

    #endregion
    }
}