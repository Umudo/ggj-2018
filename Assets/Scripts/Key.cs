using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isOn
    {
        get { return _isOn; }
        set
        {
            _isOn = value;
            if (_isOn)
            {
                print("should fire");
                _laserController.StartFire(targetPlatform);
            }
            else
            {
                print("should stop fire");
                _laserController.StopFire();
            }
        }
    }

    private Transform targetPlatform;
    private GameObject targetGameObject;

    public float laserMaxLength = 100000f;

    public GameObject relatedLock;

    private const int _cameraRayDistance = 2;
    private Camera _fpsCamera;

    private bool _isOn;

    private Lock _lock;

    private LaserController _laserController;

    // Use this for initialization
    void Start()
    {
        targetGameObject = new GameObject();
        _laserController = GetComponent<LaserController>();
        _fpsCamera = Camera.main;
        isOn = false;

        if (relatedLock != null)
        {
            _lock = relatedLock.GetComponent<Lock>();
        }
        else
        {
            throw new ArgumentException("Related Lock is not set.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        castRayFromCamera();
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000))
        {
            targetGameObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            targetPlatform = targetGameObject.transform;
        }
    }

    void castRayFromCamera()
    {
        if (relatedLock == null)
        {
            print("Related Lock is not set.");

            return;
        }

        RaycastHit hit;
        Vector3 playerForward = _fpsCamera.transform.forward;
        Debug.DrawRay(_fpsCamera.transform.position, playerForward * _cameraRayDistance, Color.green);

        int layerMask = 1 << LayerMask.NameToLayer("KeyLayer");
        if (Physics.Raycast(_fpsCamera.transform.position, playerForward, out hit, _cameraRayDistance, layerMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                isOn = !isOn;
            }
        }

        if (isOn)
        {
            ShootLaserFromTargetPosition(transform.position, transform.forward,
                laserMaxLength);
        }
        else
        {
            // Laser is off, if the interactableObj is open, close.
            if (_lock.interactState)
            {
                _lock.interactState = false;
            }
        }
    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        if (relatedLock == null)
        {
            print("Related Lock is not set.");

            return;
        }

        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);
        Debug.DrawRay(targetPosition, direction * 100, Color.cyan);
        int layerMask = 1 << LayerMask.NameToLayer("LockLayer");
        if (Physics.Raycast(targetPosition, direction, out raycastHit, laserMaxLength, layerMask))
        {
            endPosition = raycastHit.point;
            // If interactableObj is not open and the ray hits the lock
            if (!_lock.interactState && raycastHit.collider.gameObject == relatedLock)
            {
                _lock.interactState = true;
            }

            // Laser is on but we are no longer colliding with the lockObject
            if (_lock.interactState && raycastHit.collider.gameObject != relatedLock)
            {
                _lock.interactState = false;
            }
        }
        else
        {
            // We don't hit anything and the door is open.
            if (_lock.interactState)
            {
                _lock.interactState = false;
            }
        }
    }
}