using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class Bot : Character
{
    public NavMeshAgent agent;
    private Rigidbody botRigidbody;
    IState currentState;
    public Level level;
    public LayerMask brickGroundLayer;
    public Stage stage;
    public bool reachedDestination => Vector3.Distance(transform.position, destination) < 0.1f;
    public Vector3 destination;



    protected override void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        botRigidbody = GetComponent<Rigidbody>();
        ChangeState(new CollectState(stage,this));
    }

    protected override void Update()
    {

        if (currentState != null)
        {
            //Debug.Log(CanBotMoveForward());

            //if (CanBotMoveForward() == true)
            //{
                //OnDrawGizmos();
                currentState.OnExecute(this);
                base.Update();
            //}

            //else
            //{
            //    botRigidbody.velocity = Vector3.zero;
            //    return;
            //}

        }
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

    public BrickGround FindBrick(ColorType colorType)
    {
        BrickGround brick = null;
        foreach(BrickGround brickGround in Stage.Instance.bricks)
        {
            if(brickGround.colorType == colorType)
            {
                Collider[] hitColliders = new Collider[1];
                int numColliders = Physics.OverlapSphereNonAlloc(brickGround.transform.position, 10f, hitColliders, brickGroundLayer);

                if(numColliders > 0)
                {
                    brick = brickGround;
                    break;
                }
            }

        }

        return brick;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
