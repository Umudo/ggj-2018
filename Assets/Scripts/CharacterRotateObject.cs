using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotateObject : MonoBehaviour {
    public RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Physics.Raycast(ray,out hit, 10) && hit.distance <= 2){
            if (hit.transform.gameObject.GetComponent<RotatableObject>() != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    hit.transform.gameObject.GetComponent<RotatableObject>().RotateObject("right");
                if (Input.GetKeyDown(KeyCode.Q))
                    hit.transform.gameObject.GetComponent<RotatableObject>().RotateObject("left");
            }
        }
	}

}
