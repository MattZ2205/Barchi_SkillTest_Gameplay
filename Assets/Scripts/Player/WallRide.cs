using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRide : Character
{
    [Header("WallRide")]
    [SerializeField] float wallCheckDistance;
    [SerializeField] LayerMask wallLayer;

    bool wallRight;
    bool wallLeft;
    RaycastHit rightWallhit;
    RaycastHit leftWallhit;
    Vector3 initialVelocity;
    Vector3 wallForward;

    [HideInInspector] public Vector3 wallNormal;
    [HideInInspector] public bool isWallRiding;

    private void Update()
    {
        CheckForWall();
    }

    private void FixedUpdate()
    {
        StartWallRun();
    }

    public void StopWallRiding()
    {
        rb.useGravity = true;
        isWallRiding = false;
    }

    void StartWallRun()
    {
        if ((wallRight || wallLeft) && !IsGrounded && Input.GetAxis("Vertical") > 0)
        {
            //if (!isWallRiding) wallRideStartHeight = transform.position.y;
            if (!isWallRiding)
            {
                initialVelocity = rb.velocity;
                initialVelocity.y = 0;
            }

            wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
            wallForward = Vector3.Cross(wallNormal, transform.up);
            if ((transform.forward - wallForward).magnitude > (transform.forward - -wallForward).magnitude) wallForward = -wallForward;
            wallForward.y = 0;

            transform.rotation = Quaternion.LookRotation(wallForward, transform.up);

            rb.useGravity = false;
            //rb.velocity = initialVelocity;
            rb.AddForce(wallForward * speed, ForceMode.Force);

            isWallRiding = true;
        }
        else StopWallRiding();
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, transform.right, out rightWallhit, wallCheckDistance, wallLayer);
        wallLeft = Physics.Raycast(transform.position, -transform.right, out leftWallhit, wallCheckDistance, wallLayer);
    }
}
