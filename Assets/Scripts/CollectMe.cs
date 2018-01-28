using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMe : MonoBehaviour {

    RaycastHit hit;
    Transform start;
    Transform end;
    float changePositionTime = 1;
    Rigidbody rb;
    void Start () {
		
	}
	
	void Update () {
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
            if (hit.transform.gameObject.GetComponent<DropPlatform>() != null)
            {
                if (Input.GetKeyDown(KeyCode.F) && GameObject.FindObjectOfType<Player>().grabbedObject != null)
                {
                    Vector3 dropPosition = hit.transform.position;
                    Bounds groundHeight = hit.transform.gameObject.GetComponent<DropPlatform>().getHeight();
                    dropPosition.y += (groundHeight.size.y + GetComponent<Collider>().bounds.size.y) / 2 - 0.08f;
                    StartCoroutine(AlignMe(changePositionTime));
                    StartCoroutine(MoveToPosition(dropPosition, changePositionTime));
                }
            }
        
}

    public void Attach(Transform parent)
    {
        transform.parent = parent;
    }

    IEnumerator AlignMe(float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(Vector3.zero);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle,toAngle, Mathf.SmoothStep(0.0f, inTime, Mathf.SmoothStep(0.0f, inTime, t)));
            yield return null;
        }
    }

    public IEnumerator MoveToPosition( Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }

}
