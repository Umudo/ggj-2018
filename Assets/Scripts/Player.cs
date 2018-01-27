using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject pivot;
    public float raycastDistance;
    public GameObject grabbedObject;
    private bool _keyPressed;

    // Use this for initialization
    void Start()
    {
        grabbedObject = null;
        _keyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _keyPressed = true;           
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            _keyPressed = false;
        }
    }

    void FixedUpdate()
    {
        if (_keyPressed && grabbedObject == null)
        {
            CastRay();
            if (grabbedObject != null)
            {
                _keyPressed = false;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }
        else if (_keyPressed && grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            grabbedObject.transform.parent = null;
            grabbedObject = null;
            _keyPressed = false;
        }
    }

    private void CastRay()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int layerMask = 1 << LayerMask.NameToLayer("Collectible");

        Debug.DrawRay(transform.position, fwd * 2, Color.green);

        if (Physics.Raycast(transform.position, fwd, out hit, 2, layerMask))
        {
            CollectMe c = hit.transform.gameObject.GetComponent<CollectMe>();
            if (c != null)
            {
                grabbedObject = hit.transform.gameObject;
                c.Attach(transform);
            }
        }
    }

    public void setGrabbedObjectKinematic(bool kinematic)
    {
        
    }
}