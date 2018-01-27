using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour {
    public RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Physics.Raycast(ray,out hit, 10) && hit.distance <= 1.5){
            StartCoroutine(ShowRotateMessage());
            if (hit.transform.gameObject.GetComponent<RotatableObject>() != null)
            {

                if (Input.GetKeyDown(KeyCode.E))
                    hit.transform.gameObject.GetComponent<RotatableObject>().RotateObject("right");
                if (Input.GetKeyDown(KeyCode.Q))
                    hit.transform.gameObject.GetComponent<RotatableObject>().RotateObject("left");
            }
        }
	}

    IEnumerator ShowRotateMessage()
    {
        float delay = 1f;
        GetComponent<GUIText>().text = "<-                                            ->";
        GetComponent<GUIText>().enabled = true;
        yield return new WaitForSeconds(delay);
        GetComponent<GUIText>().enabled = false;
    }
}
