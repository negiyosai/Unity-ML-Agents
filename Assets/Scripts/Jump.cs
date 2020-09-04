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
    public bool isGrounded; 
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
        if (transform.position.y >= 7)
            Reset();
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
        scoreText.text = "Score: " + score;
        isGrounded = false;

        //Reset Movement and Position
        transform.position = startingPosition;
        rb.velocity = Vector3.zero;

        OnReset?.Invoke();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        
    }

    void OnCollisionExit(Collision other)
    {
        //if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Score")
        {
            AddReward(increment: 0.1f);
            score++;
            scoreText.text = "Score: " + score;
        }

        else if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Game Over");
            AddReward(increment:-1f);
            EndEpisode();

        }
    }
}
