using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {
    public WSP_LaserBeamWS LaserBeamWeaponSystem;
    public LaserLevelController LevelController;

    private bool firing;

    private Transform targetTransform;
    // Use this for initialization
    void Start()
    {
        // Check Laser Beam Weapon System
        LaserBeamWeaponSystem = gameObject.GetComponent<WSP_LaserBeamWS>();
       
        // Laser Level Controller
        LevelController = gameObject.GetComponent<LaserLevelController>();

        firing = false;
    }

    // Update is called once per frame
    void Update () {
        if (firing)
        {
            if (LaserBeamWeaponSystem.CurrentTarget != targetTransform)
                LaserBeamWeaponSystem.CurrentTarget = targetTransform;
            LaserBeamWeaponSystem.FireLaser();
        }
        else
        {
            if (LaserBeamWeaponSystem.LaserFiring)
                LaserBeamWeaponSystem.StopLaserFire();
        }
	}


    public void StopFire()
    {
        firing = false;
    }

    public void StartFire(Transform targetTransform)
    {
        Debug.Log(targetTransform);
        print("in start fire");
        this.targetTransform = targetTransform;
        firing = true;

    }
}
