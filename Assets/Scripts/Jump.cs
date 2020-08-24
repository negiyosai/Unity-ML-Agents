using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System;
using UnityEngine.UI;

public class Jump : Agent
{
    public float jumpHeight = 7f;
    public Text scoreText;
    bool isGrounded; 
    Rigidbody rb;
    Vector3 startingPosition;
    int score = 0;


    public event Action OnReset;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        scoreText.text = "Score: " + score;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (Mathf.FloorToInt(vectorAction[0]) == 1)
            TriggerJump();
    }

    public override void OnEpisodeBegin()
    {
        Reset();
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        if (Input.GetKey(KeyCode.Space))
            actionsOut[0] = 1;


    }

    private void FixedUpdate()
    {
        if (isGrounded)
            RequestDecision();
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

    private void Reset()
    {
        score = 0;
        isGrounded = true;

        //Reset Movement and Position
        transform.position = startingPosition;
        rb.velocity = Vector3.zero;

        OnReset?.Invoke();
    }

    void BoolChange()
    {
        isGrounded = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Invoke("BoolChange", 0.1f);
        }
        
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Score")
        {
            score++;
            scoreText.text = "Score: " + score;
        }
    }
}
