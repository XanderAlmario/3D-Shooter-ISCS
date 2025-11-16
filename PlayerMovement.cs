using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public CapsuleCollider playerCollider;

    public float speed = 15.5f,
    smoothTurningTime = 0.1f, smoothTurningVel,
    jump = 15.5f, gravity = -9.81f,
    camX, camY, camOffsetY, origCamY;

    public bool grounded = true;
    public float vertForce = 0f;
    public Transform cam;

    void Start()
    {
        origCamY = cam.transform.position.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        animator.SetFloat( "Vertical", Mathf.Abs( vertical ) );
        animator.SetFloat( "Horizontal", Mathf.Abs( horizontal ) );

        camX += Input.GetAxis("Mouse X");
        camY += Input.GetAxis("Mouse Y");
        camOffsetY = origCamY - (camY / 20);

        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward = transform.forward.normalized;
        camRight = transform.right.normalized;

        Vector3 relativeForward = vertical * camForward;
        Vector3 relativeRight = horizontal * camRight;

        Vector3 direction = relativeForward + relativeRight;

        cam.transform.rotation = Quaternion.Euler(-camY, camX, 0f);
        transform.rotation = Quaternion.Euler(0f, camX, 0f);
        cam.transform.position = new Vector3(cam.transform.position.x, camOffsetY, cam.transform.position.z);

        controller.Move(direction * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift)) speed = 35f;
        else speed = 15.5f;

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!grounded) vertForce = jump;
        }*/
    }

    void OnCollisionEnter(Collision other)
    {
        grounded = true;
    }

    void OnCollisionStay(Collision other)
    {
        grounded = true;
    }
    
    void OnCollisionExit(Collision other)
    {
        grounded = false;
    }
}

