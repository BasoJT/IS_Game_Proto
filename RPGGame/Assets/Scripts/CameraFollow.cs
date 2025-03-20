using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float camspeed = 2f;
    public Transform target;//ie player

    // Update is called once per frame
    void Update()
    {
        Vector3 playerpos = new Vector3(target.position.x, target.position.y, -10);
        transform.position = Vector3.Slerp(transform.position, playerpos, camspeed*Time.deltaTime);
    }
}
