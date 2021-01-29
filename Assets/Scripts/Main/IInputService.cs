using UnityEngine;

namespace Main
{
    public interface IInputService
    {
    #region Public Methods

        bool IsKeyDown(KeyCode     keycode);
        bool IsKeyUp(KeyCode       keycode);
        void RegisterKey(KeyCode   keycode);
        void UnRegisterKey(KeyCode keycode);

    #endregion
    }
}