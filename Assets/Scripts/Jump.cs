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
    int penalty = 0;


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
                penalty += 1;
                Debug.Log("JUMP");
                rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.VelocityChange);
                isGrounded = false;
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
            if (penalty >= 1)
            {
               // AddReward(-1f);
                penalty = 0;
            }
            isGrounded = true;
            
        }
        
    }

    /*void OnCollisionExit(Collision other)
    {
        //if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Score")
        {
            AddReward(0.1f);
            score++;
            scoreText.text = "Score: " + score;
            penalty = 0;
        }

        else if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Game Over");
            AddReward(-1f);
            penalty = 0;
            EndEpisode();
        }
    }
}
