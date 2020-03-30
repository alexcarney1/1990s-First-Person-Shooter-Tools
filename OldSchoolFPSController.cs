using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Author: Alex R. Carney
* Player controller for 1990s-esque FPS games. Smooth and fast feel like 
* Doom, Duke Nukem 3D, etc. 
* 
* Altering speed and rigidbody drag is best way to change feel!
* Old school strafe run! Speed increase enabled when moving forward + to the side.
* NOTE: Can only look left and right, like many FPS games from the 1990s!
* 
* My rigidbody settings: 1 mass -- 2.2 drag -- 0 angular drag
* Main Camera child of player GameObject w/ capsule collider + rigidbody attached. 
* 
*/
public class OldSchoolFPSController : MonoBehaviour
{
    private float mouseRotX;
    public float MouseSens =1f;
    private Rigidbody rb;
    public float baseSpeed = 30.0f;
    private float speed;
    public bool canStrafeRun = true;
    //% increase in speed when strafe running
    public float strafeRunMultiplier = .30f; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        speed = baseSpeed;
    }

    private void Update()
    {
        MouseLook();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void MouseLook()
    {
        mouseRotX += Input.GetAxisRaw("Mouse X") * MouseSens;
        transform.rotation = Quaternion.Euler(0f, mouseRotX, 0f); //Look L R
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveVector = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
        moveVector = transform.rotation * moveVector; //W is always forward with this key line
        if (canStrafeRun){ StrafeHandler(); }

        rb.AddForce(moveVector * speed, ForceMode.Force);
    }

    public void StrafeHandler()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            speed = baseSpeed + (baseSpeed * strafeRunMultiplier);
            return;
        }
        else
        {
            speed = baseSpeed;
            return;
        }
    }
}
