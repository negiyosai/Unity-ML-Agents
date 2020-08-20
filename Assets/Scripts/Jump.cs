using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System;

public class Jump : Agent
{
    public float jumpHeight = 7f;
    public bool isGrounded;

    private Rigidbody rb;

    public event Action OnReset;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();

    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (Mathf.FloorToInt(vectorAction[0]) == 1)
            TriggerJump();
    }

    public override void OnEpisodeBegin()
    {
        //Reset
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        if (Input.GetKey(KeyCode.Space))
            actionsOut[0] = 1;


    }

    void Update()
    {
        //TriggerJump();
    }

    void TriggerJump()
    {
        if (isGrounded)
        {
            //if (Input.GetKeyDown("space"))
            {
                rb.AddForce(Vector3.up * jumpHeight * 100);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Invoke("BoolChange", 0.1f);
        }
    }

    void BoolChange()
    {
        isGrounded = true;
    }


    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
