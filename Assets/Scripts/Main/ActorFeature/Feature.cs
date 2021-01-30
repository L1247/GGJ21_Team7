using Main.Input;
using UnityEngine;

namespace Main.ActorFeature
{
    public class Feature : MonoBehaviour
    {
        protected Actor         _actor;
        protected IInputService _inputService;
        private void Awake()
        {
            _inputService = GetComponent<IInputService>();
            _actor        = GetComponent<Actor>();
        }
    }
}