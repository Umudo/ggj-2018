using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class Lock : MonoBehaviour
{

	public GameObject interactableGameObject;

	public bool interactState
	{
		get { return _interactState;}
		set
		{
			_interactState = value;
			if (_interactable != null)
			{
				_interactable.interact(value);
			}
		}
	}

	private bool _interactState;

	private IInteractable _interactable;

	// Use this for initialization
	void Start ()
	{
        interactState = false;
        if (checkInteractableObjectImplementsInterface())
		{
			_interactable = interactableGameObject.GetComponent<IInteractable>();
		}
		else
		{
			print("Interactee Object Does Not Implement IInteractable");
		}
		
		
	}

	bool checkInteractableObjectImplementsInterface()
	{
		var interacter = interactableGameObject.GetComponent<IInteractable>();
		return interacter != null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
