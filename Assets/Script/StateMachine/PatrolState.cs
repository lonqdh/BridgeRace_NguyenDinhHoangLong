using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : IState
{
    int bricksToCollect;
    public void OnEnter(Bot bot)
    {
        bricksToCollect = Random.Range(2, 5);

        Tasking(bot); //0
    }
    public void OnExecute(Bot bot)
    {
        if(bot.reachedDestination) //2
        {
            if (bot.brickCharList.Count >= bricksToCollect)
            {
                bot.ChangeState(new CompeteState());
            }
            else
            {
               Tasking(bot);
            }
        }
    }
    public void OnExit(Bot bot)
    {

    }

    private void Tasking(Bot bot) // dung de assign nhiem vu can lam cho bot
    {
        BrickGround brick = bot.LocateForBrick(bot.colorType);
        if (brick == null)
        {
            bot.ChangeState(new CompeteState());
        }
        else
        {
            bot.GoGetBrick(brick.transform.position); //1
        }
    }
}
