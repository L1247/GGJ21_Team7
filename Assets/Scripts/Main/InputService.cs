using System;
using UnityEngine;

namespace Main
{
    public class InputService : MonoBehaviour , IInputService
    {
        [SerializeField]
        private KeyCode JumpKey = KeyCode.Space;

        private bool _jumpDown;
        private bool _jumpUp;

    #region Public Methods

        public bool IsJumpDown() => _jumpDown;
        public bool IsJumpUp()   => _jumpUp;

        public bool IsLeftArrowDown()
        {
            throw new NotImplementedException();
        }

        public bool IsLeftArrowUp()
        {
            throw new NotImplementedException();
        }

        public bool IsRightArrowDown()
        {
            throw new NotImplementedException();
        }

        public bool IsRightArrowUp()
        {
            throw new NotImplementedException();
        }

    #endregion

    #region Private Methods

        private void Update()
        {
            _jumpDown = Input.GetKeyDown(JumpKey);
            _jumpUp   = Input.GetKeyUp(JumpKey);
        }

    #endregion
    }
}