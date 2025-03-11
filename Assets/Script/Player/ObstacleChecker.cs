using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    public Vector3 rayOffset = new Vector3(0, 0.2f, 0);
    public float rayLength = 0.9f;
    public float heightRayLength = 6f;
    public LayerMask obstacleLayer;

    public ObstacleInfo CheckObstacle()
    {
        var hitdata = new ObstacleInfo();

        var rayOrigin = transform.position + rayOffset;
        hitdata.hitFound = Physics.Raycast(rayOrigin, transform.forward, out hitdata.hitInfo, rayLength, obstacleLayer);

        Debug.DrawRay(rayOrigin, transform.forward * rayLength, (hitdata.hitFound) ? Color.red : Color.green);

        if (hitdata.hitFound)
        {
            var heightOrigin = hitdata.hitInfo.point + Vector3.up * heightRayLength + transform.forward * 0.2f;
            hitdata.heightHitFound =  Physics.Raycast(heightOrigin, Vector3.down, out hitdata.heightInfo, heightRayLength, obstacleLayer);

            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (hitdata.heightHitFound) ? Color.red : Color.green);
        }
        return hitdata;
    }
}

public struct ObstacleInfo
{
    public bool hitFound;
    public bool heightHitFound;
    public RaycastHit hitInfo;
    public RaycastHit heightInfo;
}
