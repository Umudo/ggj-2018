using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{	
	public bool isOn
	{
		get { return _isOn;  }
		set
		{
			_isOn = value;
			laserLineRenderer.enabled = value;
		}
	}

	public LineRenderer laserLineRenderer;
	public float laserWidth = 0.1f;
	public float laserMaxLength = 5f;
	
	public GameObject relatedLock;

	private const int _cameraRayDistance = 2;
	private Camera _fpsCamera;
	private bool _isOn;
	private Lock _lock;

	// Use this for initialization
	void Start ()
	{
		_fpsCamera = Camera.main;

		laserLineRenderer = GetComponent<LineRenderer>();
		isOn = false;
		
		Vector3[] initLaserPositions = new Vector3[ 2 ] { Vector3.zero, Vector3.zero };
		laserLineRenderer.SetPositions( initLaserPositions );
		laserLineRenderer.startColor = Color.red;
		laserLineRenderer.endColor = Color.red;
		laserLineRenderer.startWidth = laserWidth;
		laserLineRenderer.endWidth = laserWidth;
		laserLineRenderer.enabled = isOn;
		
		if (relatedLock != null)
		{
			_lock = relatedLock.GetComponent<Lock>();
		}
		else
		{
			print("Related Lock is not set.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		castRayFromCamera();
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
          
			if (Input.GetKeyDown(KeyCode.E))
			{
				isOn = !isOn;
			}
			
		}

		if (isOn)
		{
			ShootLaserFromTargetPosition( transform.position, -1* transform.forward, laserMaxLength );
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
	
	void ShootLaserFromTargetPosition( Vector3 targetPosition, Vector3 direction, float length )
	{
		if (relatedLock == null)
		{
			print("Related Lock is not set.");
			
			return;
		}
		
		
		RaycastHit raycastHit;
		Vector3 endPosition = targetPosition + ( length * direction );
	    Debug.DrawRay(targetPosition, direction * 100, Color.cyan);
	    int layerMask = 1 << LayerMask.NameToLayer("LockLayer");
        if (Physics.Raycast(targetPosition, direction, out raycastHit,100, layerMask)) {
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
 
		laserLineRenderer.SetPosition( 0, targetPosition );
		laserLineRenderer.SetPosition( 1, endPosition );
	}
}
