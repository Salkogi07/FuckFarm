using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject targetObject;
    public float distanceX, distanceY, distanceZ, speed;

    Vector3 pos;

    private void Update()
    {
        pos = new Vector3 (targetObject.transform.position.x + distanceX, targetObject.transform.position.y + distanceY, targetObject.transform.position.z + distanceZ);
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
