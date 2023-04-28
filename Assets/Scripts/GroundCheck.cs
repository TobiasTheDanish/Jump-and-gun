using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float rayDistance;

    [SerializeField] private bool isGrounded;

    public bool IsGrounded { get => isGrounded; private set => isGrounded = value; }


    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, new Vector2(0f, -rayDistance), Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, whatIsGround);
        IsGrounded = hit;
    }
}
