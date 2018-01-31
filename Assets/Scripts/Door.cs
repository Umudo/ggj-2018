using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Door : MonoBehaviour, IInteractable
    {
        public void interact(bool state)
        {
            gameObject.GetComponent<Animator>().SetTrigger("toggle");
            if (state)
            {
                print("Open door");
            }
            else
            {
                print("Close Door");
            }
        }
    }
}