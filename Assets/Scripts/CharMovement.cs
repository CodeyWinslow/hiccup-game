using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharMovement : Movement
{
    //State Vars
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public float gravity;
    [SerializeField]
    public float jumpSpeed;

    CharacterController cont;
    GroundDetection grounded;
    CharCombat combatBehavior;
    CameraFollow camFol;
    Vector3 move;
    float yspeed;
    bool jumping = false;

    //Monobehavior Lifecycle
    private void Awake()
    {
        cont = GetComponent<CharacterController>();
        Asserts.AssertNotNull(cont, "Player must have CharacterController component");
        camFol = Camera.main.GetComponent<CameraFollow>();
        Asserts.AssertNotNull(camFol, "Main camera must have CameraFollow component");
        grounded = GetComponentInChildren<GroundDetection>();
        Asserts.AssertNotNull(grounded, "Player must have GroundDetection component");
        combatBehavior = GetComponentInParent<CharCombat>();
        Asserts.AssertNotNull(combatBehavior, "Player must have CharCombat component");

        move = Vector3.zero;
        yspeed = 0;
    }

    void Update()
    {
        //get raw movement input
        move = Vector3.zero;
        move.z = Input.GetAxis("Vertical");
        move.x = Input.GetAxis("Horizontal");

        //check if trying to jump
        if (Input.GetButtonDown("Jump")) jumping = true;
    }

    private void FixedUpdate()
    {
        if (grounded == null || camFol == null) return;

        if (combatBehavior.CanMove)
        {
            //adjust to camera forward
            float x = move.x;
            float z = move.z;
            move = Vector3.zero;
            move += z * camFol.camForward;
            move += x * Vector3.Cross(Vector3.up, camFol.camForward);

            //rotate to direction
            Vector3 nextRot = transform.rotation.eulerAngles;

            float desiredRotation = nextRot.y;
            if (move.magnitude > 0)
                desiredRotation = Quaternion.LookRotation(move, Vector3.up).eulerAngles.y;

            nextRot.y = desiredRotation;
            Quaternion newRot = Quaternion.Euler(nextRot);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 0.2f);

            //normalize only if needed (allows slow movement)
            if (move.magnitude > 1)
                move.Normalize();
        }
        else
        {
            move = Vector3.zero;
        }

        if (grounded.grounded)
        {
            if (jumping)
            {
                yspeed = jumpSpeed;
                grounded.TempDisable();
            }
            else
                yspeed = 0;
        }
        else
            yspeed -= gravity * Time.deltaTime;

        jumping = false;

        //apply movespeed and yspeed
        move *= moveSpeed;
        move.y = yspeed;

        //move
        cont.Move(move * Time.deltaTime);
    }

    public override void ChangeSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
