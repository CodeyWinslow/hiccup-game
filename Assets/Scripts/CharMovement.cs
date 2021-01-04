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
    Animator anim;
    CharCombat combatBehavior;
    EnemyTargeting targeting;
    CameraFollow camFol;
    Vector3 move;
    Vector3 velocity;
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
        anim = GetComponentInChildren<Animator>();
        Asserts.AssertNotNull(anim, "Player must have an Animator component");
        combatBehavior = GetComponent<CharCombat>();
        Asserts.AssertNotNull(combatBehavior, "Player must have CharCombat component");
        targeting = GetComponentInChildren<EnemyTargeting>();
        Asserts.AssertNotNull(targeting, "Player must have EnemyTarget component");

        move = Vector3.zero;
        velocity = Vector3.zero;
        yspeed = 0;
    }

    void Update()
    {
        //get raw movement input
        move = Vector3.zero;
        move.z = Input.GetAxis("Vertical");
        move.x = Input.GetAxis("Horizontal");

        //animate movement
        anim.SetFloat("RunningForward", Vector3.Dot(velocity/moveSpeed, transform.forward));
        anim.SetFloat("Strafe", Vector3.Dot(velocity/moveSpeed, transform.right));

        //check if trying to jump
        if (Input.GetButtonDown("Jump")) jumping = true;
    }

    private void FixedUpdate()
    {
        if (grounded == null || camFol == null) return;

        if (combatBehavior.CanMove)
        {
            //adjust to camera forward
            move = DesiredDirectionFromCamera(move);

            //rotate to direction
            if (targeting.Targeting && targeting.HasTarget)
                RotatePlayerToDirection(targeting.TargetTransform.position - transform.position);
            else
                RotatePlayerToDirection(move);
            
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
        velocity = moveSpeed * move;
        velocity.y = yspeed;

        //move
        cont.Move(velocity * Time.deltaTime);
    }

    Vector3 DesiredDirectionFromCamera(Vector3 input)
    {
        float x = input.x;
        float z = input.z;
        input = Vector3.zero;
        input += z * camFol.camForward;
        input += x * Vector3.Cross(Vector3.up, camFol.camForward);

        return input;
    }

    void RotatePlayerToDirection(Vector3 dir)
    {
        if (dir.magnitude == 0)
            return;

        Vector3 nextRot = transform.rotation.eulerAngles;

        float desiredRotation = nextRot.y;

        desiredRotation = Quaternion.LookRotation(dir, Vector3.up).eulerAngles.y;

        nextRot.y = desiredRotation;

        Quaternion newRot = Quaternion.Euler(nextRot);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 0.2f);
    }

    public override void ChangeSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
