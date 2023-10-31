using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompeteState : IState
{
    private void OnEnter(Bot bot)
    {
        //test

        bot.GoToFinishLine();
    }
    private void OnExecute(Bot bot)
    {
        if(bot.brickCharList.Count == 0)
        {
            bot.ChangeState(new PatrolState());
        }
    }
    private void OnExit(Bot bot)
    {

    }
}
