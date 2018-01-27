using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, t)));
            yield return null;
        }
    }

    public void RotateObject(string rotation)
    {
        Vector3 direction;
        if (rotation == "right")
            direction = -Vector3.up;
        else
            direction = Vector3.up;
        StartCoroutine(RotateMe(direction * 45, 1));
    }
}
