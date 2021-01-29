using System.Collections.Generic;
using UnityEngine;

namespace Main.Input
{
    public class InputService : MonoBehaviour , IInputService
    {
    #region Private Variables

        private readonly List<Key> keycodes = new List<Key>();

    #endregion

    #region Public Methods

        public bool IsKeyDown(KeyCode keycode)
        {
            var isKeyDown = false;
            if (HasKey(keycode , out var key))
                isKeyDown = key.KeyDown;
            return isKeyDown;
        }

        public bool IsKeyPress(KeyCode keycode)
        {
            var isKeyPress = false;
            if (HasKey(keycode , out var key))
                isKeyPress = key.KeyPress;
            return isKeyPress;
        }


        public bool IsKeyUp(KeyCode keycode)
        {
            var isKeyUp = false;
            if (HasKey(keycode , out var key))
                isKeyUp = key.KeyUp;
            return isKeyUp;
        }


        public void RegisterKey(KeyCode keycode)
        {
            if (HasKey(keycode , out var key) == false)
            {
                var newKey = new Key(keycode);
                keycodes.Add(newKey);
            }
        }

        public void UnRegisterKey(KeyCode keycode)
        {
            if (HasKey(keycode , out var key))
                keycodes.Remove(key);
        }

    #endregion

    #region Private Methods

        private bool HasKey(KeyCode keycode , out Key key)
        {
            key = keycodes.Find(k => k.KeyCode == keycode);
            var hasKey = key != null;
            return hasKey;
        }

        private void Update()
        {
            foreach (var key in keycodes)
            {
                key.KeyPress = UnityEngine.Input.GetKey(key.KeyCode);
                key.KeyDown  = UnityEngine.Input.GetKeyDown(key.KeyCode);
                key.KeyUp    = UnityEngine.Input.GetKeyUp(key.KeyCode);
            }
        }

    #endregion
    }

    internal class Key
    {
    #region Public Variables

        public bool KeyDown  { get; set; }
        public bool KeyPress { get; set; }
        public bool KeyUp    { get; set; }

        public KeyCode KeyCode { get; }

    #endregion

    #region Constructor

        public Key(KeyCode keycode)
        {
            KeyCode = keycode;
        }

    #endregion
    }
}