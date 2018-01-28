using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITAsanso : MonoBehaviour, Assets.Scripts.Interfaces.IInteractable
{
    public void interact(bool state)
    {
        gameObject.GetComponentInChildren<Animator>().SetTrigger("toggle");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
