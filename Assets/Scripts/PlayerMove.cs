using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 targetPositon;
    private bool hasTarget = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                animator.Play("Move");
                targetPositon = hit.point;
                hasTarget = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector3 direction = (targetPositon - rb.position);
            direction.y = 0;
            direction.Normalize();

            if(Vector3.Distance(new Vector3(rb.position.x, 0, rb.position.z),
                new Vector3(targetPositon.x, 0, targetPositon.z)) < 0.5f)
            {
                animator.Play("Idle");
                hasTarget = false;
            }
            else
            {
                animator.Play("Move");
                rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
                transform.LookAt(new Vector3(targetPositon.x, transform.position.y, targetPositon.z));
            }
        }
    }
}
