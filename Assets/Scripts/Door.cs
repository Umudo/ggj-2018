﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void interact(bool state)
	{
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
