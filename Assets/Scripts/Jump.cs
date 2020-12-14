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
    public bool isGrounded; 
    Rigidbody rb;
    Vector3 startingPosition;
    public int penalty = 0;
    public int penaltyAmount = 0;
    public Color hitColor;
    public Color originalColor;
    public event Action OnReset;

    private void Start()
    {
        
    }


    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
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
        ManagerScript.instance.score = 0;
        ManagerScript.instance.scoreText.text = "Score: " + ManagerScript.instance.score;
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
                AddReward(-0.2f); //Negative reward for jumping without any reason
                penalty = 0;
                penaltyAmount -= 1;
            }
            isGrounded = true;
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Score")
        {
            AddReward(0.1f);
            penaltyAmount += 1;
            ManagerScript.instance.score++;
            ManagerScript.instance.scoreText.text = "Score: " + ManagerScript.instance.score;
            penalty = 0;
        }

        else if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Game Over");
            gameObject.GetComponent<Renderer>().material.color = hitColor;
            Invoke("ChangeColor", 1f);
            AddReward(-0.8f);
            penalty = 0;
            EndEpisode();
        }
    }

    void ChangeColor()
    {
        gameObject.GetComponent<Renderer>().material.color = originalColor;
    }
}
