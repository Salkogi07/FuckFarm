using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelPos : MonoBehaviour
{
    public Transform targetPos;
    public Transform camPos;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMove>().hasTarget = false;
            other.GetComponent<PlayerMove>().targetPositon = targetPos.position;
            other.GetComponent<PlayerMove>().animator.Play("Idle");

            camPos.position = targetPos.position + new Vector3(0,10,0);
            other.gameObject.transform.position = targetPos.position;
        }
    }
}
