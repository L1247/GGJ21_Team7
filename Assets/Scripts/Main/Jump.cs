using System;
using UnityEngine;

namespace Main
{
    public class Jump : MonoBehaviour
    {
        private IInputService _inputService;
        private Actor         _actor;
        [SerializeField]
        private float JumpForce;

        [SerializeField]
        private KeyCode jumpKeyCode = KeyCode.Space;

        private void Awake()
        {
            _inputService = GetComponent<IInputService>();
            _actor        = GetComponent<Actor>();
        }

        private void Start()
        {
            _inputService.RegisterKey(jumpKeyCode);
        }

        private void Update()
        {
            if (_inputService.IsKeyDown(jumpKeyCode)) _actor.Jump(JumpForce);
        }
    }
}