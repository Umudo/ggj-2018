using UnityEngine;

namespace Assets.Scripts
{
    public class Key : MonoBehaviour
    {
        private bool _isOn;

        public bool isOn
        {
            get { return _isOn; }
            set
            {
                _isOn = value;
                if (_isOn)
                {
                    print("should fire");
                    _laserController.StartFire(_targetGameObject.transform);
                }
                else
                {
                    print("should stop fire");
                    _laserController.StopFire();
                }
            }
        }

        private GameObject _targetGameObject;
        private float _rayDistance = 1000f;
        private LaserController _laserController;

        // Use this for initialization
        void Start()
        {
            _lastFoundLock = null;
            _targetGameObject = new GameObject();
            _laserController = GetComponent<LaserController>();
            isOn = false;
        }

        void FixedUpdate()
        {
            FindLaserEndpoint();
            FindLock();
            Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.red);
        }

        public void Toogle()
        {
            isOn = !isOn;
        }

        private Lock _lastFoundLock;

        void FindLock()
        {
            RaycastHit raycastHit;

            Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.cyan);
            int layerMask = 1 << LayerMask.NameToLayer("LockLayer");
            if (Physics.Raycast(transform.position, transform.forward, out raycastHit, _rayDistance, layerMask))
            {
                if (_lastFoundLock != raycastHit.collider.gameObject.GetComponent<Lock>())
                {
                    _lastFoundLock.Closed(gameObject);
                    _lastFoundLock = raycastHit.collider.gameObject.GetComponent<Lock>();
                }

                _lastFoundLock.Opened(gameObject);
            }
            else
            {
                _lastFoundLock.Closed(gameObject);
            }
        }

        private void FindLaserEndpoint()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, _rayDistance))
            {
                _targetGameObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
        }
    }
}