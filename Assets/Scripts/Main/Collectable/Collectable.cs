using System;
using UnityEngine;

namespace Main.Collectable
{
    public class Collectable : MonoBehaviour
    {
    #region Private Variables

        private GameObject _triggerActor;


        private readonly string _player = "Player";

        [SerializeField]
        private Sprite _featureSprite;

        [SerializeField]
        private string _featureComponent;

    #endregion

    #region Private Methods

        [ContextMenu("Addtype")]
        private void AddType()
        {
            var type = Type.GetType("Main.ActorFeature." + _featureComponent);
            // Debug.Log($"{type}");
            _triggerActor.GetComponent<Actor>().AddComponent(type , _featureSprite);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D actorCollider2D)
        {
            if (actorCollider2D.CompareTag(_player))
            {
                _triggerActor = actorCollider2D.gameObject;
                AddType();
            }
        }

    #endregion
    }
}