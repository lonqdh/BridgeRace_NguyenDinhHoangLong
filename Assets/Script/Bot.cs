using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public class Bot : Character
{
    public NavMeshAgent agent;
    IState currentState;

    private Vector3 destination;
    public Level level;
    public bool reachedDestination => Vector3.Distance(transform.position, destination) < 0.1f;
    public Stage stage;
    private Rigidbody botRigidbody;

    public void GoGetBrick(Vector3 destination) //ham dieu chinh bot di nhat brick
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    public void GoToFinishLine()
    {
        agent.SetDestination(level.finishPoint.position);
    }

    public void ChangeState(IState state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal BrickGround LocateForBrick(ColorType colorType) //ham de ket hop xac dinh vi tri cua brick cung mau can di nhat
    {
        BrickGround brick = null;
        for (int i = 0; i < stage.bricks.Count; i++) //*
        {
            if (stage.bricks[i].colorType == colorType)
            {
                brick = stage.bricks[i];

                break;
            }
        }

        return brick;
    }

    protected override void Start()
    {
        //base.Start()
        //stage = GameObject.FindObjectOfType<Stage>();
        agent = GetComponent<NavMeshAgent>();
        botRigidbody = GetComponent<Rigidbody>();
        ChangeState(new PatrolState());
    }

    protected virtual void Update()
    {
        if (currentState != null)
        {
            if(CanBotMoveForward() == true)
            {
                currentState.OnExecute(this);
                base.Update();
            }
            
            else
            {
                botRigidbody.velocity = Vector3.zero;
                return;
            }

        }
    }
    public bool CanBotMoveForward()
    {
        // Get the bot's rigidbody

        // Check if the bot is moving forward (velocity.z > 0)
        if (this.botRigidbody.velocity.z > 0)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + new Vector3(0f, 0f, 0.5f), Vector3.down, out hit, 5f, brickStairLayer))
            {
                BrickStair brickStair = hit.collider.gameObject.GetComponent<BrickStair>();

                // Check if the bot has no bricks and the brick color doesn't match its color
                if (brickCharList.Count == 0 && brickStair.colorType != this.colorType)
                {
                    return false; // The bot can't move forward
                }
            }

            // If none of the above conditions are met, the bot can move forward
            return true;
        }

        // The bot is not moving forward, so it can move
        return true;
    }

    internal void OnInit()
    {
        
    }
}
