using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    private Animator _animator;

	// Use this for initialization
	void Start () {

        _animator = GetComponent<Animator>();

        toggle();
	}
	
    public void toggle()
    {
        _animator.SetTrigger("toggle");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
