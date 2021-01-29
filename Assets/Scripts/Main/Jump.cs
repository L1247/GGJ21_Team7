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

        private void Awake()
        {
            _inputService = GetComponent<IInputService>();
            _actor        = GetComponent<Actor>();
        }

        private void Update()
        {
            if (_inputService.IsJumpDown()) _actor.Jump(JumpForce);
        }
    }
}