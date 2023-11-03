using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectState : IState
{
    private Stage stage;
    private Bot bot;

    public CollectState(Stage stage, Bot bot)
    {
        this.stage = stage;
        this.bot = bot;
    }

    int bricksToCollect;

    public void OnEnter(Bot bot)
    {
        bricksToCollect = Random.Range(3, 6);
    }

    public void OnExecute(Bot bot)
    {
        if (Stage.Instance.bricks.Count > 0)
        {
            //if (bot.reachedDestination)
            //{
            if (bot.brickCharList.Count >= bricksToCollect)
            {
                bot.ChangeState(new CompeteState(bot, stage));
                Debug.Log("DaDuGach");
            }
            else
            {
                BrickGround brickFound = bot.FindBrick(bot.colorType);
                if(brickFound != null)
                {
                    GoGetBrick(brickFound.transform.position);
                }
                else
                {
                    bot.ChangeState(new CompeteState(bot, stage));
                }
                
            }
            //}
        }
        else
        {
            bot.ChangeState(new CompeteState(bot, stage));
            Debug.Log("No bricks left found in the stage.");
        }

    }

    public void OnExit(Bot bot)
    {
        // Cleanup or exit logic
    }


    //public BrickGround LocateForBrick(ColorType colorType)
    //{
    //    BrickGround brick = null;
    //    //Debug.Log(bricks.Count + " OK");
    //    for (int i = 0; i < Stage.Instance.bricks.Count; i++)
    //    {
    //        if (Stage.Instance.bricks[i].colorType == colorType)
    //        {
    //            brick = Stage.Instance.bricks[i];
    //            //Debug.Log("ok");
    //            break;
    //        }
    //    }
    //    return brick;
    //}


    //private BrickGround LocateForBrick(ColorType colorType)
    //{
    //    BrickGround brick = null;

    //    if (stage.colorBricksDictionary.TryGetValue(colorType, out List<BrickGround> bricksForColor))
    //    {
    //        foreach (BrickGround brickGround in bricksForColor)
    //        {
    //            Collider[] hitColliders = new Collider[1];
    //            int numColliders = Physics.OverlapSphereNonAlloc(brickGround.transform.position, 10f, hitColliders, bot.brickGroundLayer);

    //            if (numColliders > 0)
    //            {
    //                brick = brickGround;
    //                break;
    //            }
    //        }
    //    }

    //    return brick;
    //}


    private void GoGetBrick(Vector3 destination)
    {
        bot.destination = destination;
        bot.agent.SetDestination(destination);
    }
}
