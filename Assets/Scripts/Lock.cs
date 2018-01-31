using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Lock : MonoBehaviour
    {
        public GameObject Interactable;
        private IInteractable _interactable;

        void Start()
        {
            _interactable = Interactable.GetComponent<IInteractable>();
            if (_interactable == null)
            {
                print("Interactable object does not implement IInteractable interface");
            }
        }
        public void Opened(GameObject o)
        {
            _interactable.interact(true);
        }

        public void Closed(GameObject p0)
        {
            _interactable.interact(false);
        }
    }
}
