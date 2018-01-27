using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1Control : MonoBehaviour {



    private Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = gameObject.GetComponent<Animator>();
        //IsOpen = true
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
