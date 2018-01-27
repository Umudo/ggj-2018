using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject pivot;
    public float raycastDistance;

    private GameObject _grabbedObject;
    private bool _keyPressed;

    // Use this for initialization
    void Start()
    {
        _grabbedObject = null;
        _keyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _keyPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (_keyPressed && _grabbedObject == null)
        {
            CastRay();
            if (_grabbedObject != null)
            {
                _keyPressed = false;
                _grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                _grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }
        else if (_keyPressed && _grabbedObject != null)
        {
            _grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            _grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            _grabbedObject.transform.parent = null;
            _grabbedObject = null;
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
                _grabbedObject = hit.transform.gameObject;
                c.Attach(transform);
            }
        }
    }
}