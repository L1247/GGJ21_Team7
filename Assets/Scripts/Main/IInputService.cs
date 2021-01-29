using UnityEngine;

namespace Main
{
    public interface IInputService
    {
        void RegisterKey(KeyCode keycode);
        void UnRegisterKey(KeyCode keycode);
        bool IsKeyDown(KeyCode keycode);
        bool IsKeyUp(KeyCode  keycode);
    }
}