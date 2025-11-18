using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCM : MonoBehaviour
{
    public float speed = 10, groundYOffset = 0.6f, gravity = -9.81f;
    private float horizontal, vertical;

    public Vector3 spherePos, dir;
    public CharacterController controller;
    public LayerMask ground;

    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        GetDirAndMove();
        Gravity();
    } 

    private void GetDirAndMove()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        dir = transform.forward * vertical + transform.right * horizontal;

        controller.Move(dir * speed * Time.deltaTime);
    }

    private bool IsGrounded()
    {
       spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
       if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, ground)) return true;
       else return false; 
    }

    private void Gravity()
    {
        if(!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
