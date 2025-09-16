using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform target;

    private void Awake()
    {
        target = Camera.main.gameObject.transform;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(target.forward, target.up);
    }
}
