using UnityEngine;

namespace Main.Input
{
    public interface IInputService
    {
    #region Public Methods

        bool IsKeyDown(KeyCode     keycode);
        bool IsKeyPress(KeyCode    keycode);
        bool IsKeyUp(KeyCode       keycode);
        void RegisterKey(KeyCode   keycode);
        void UnRegisterKey(KeyCode keycode);

    #endregion
    }
}