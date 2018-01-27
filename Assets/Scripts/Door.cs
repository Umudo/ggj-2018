using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {

    private bool isCurrentStateOn = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
