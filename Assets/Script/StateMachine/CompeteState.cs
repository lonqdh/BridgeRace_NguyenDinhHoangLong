using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompeteState : IState
{
    //private void OnEnter(Bot bot)
    //{
    //    //test

    //    bot.GoToFinishLine();
    //}
    //private void OnExecute(Bot bot)
    //{
    //    if(bot.brickCharList.Count == 0)
    //    {
    //        bot.ChangeState(new CollectState());
    //    }
    //}
    //private void OnExit(Bot bot)
    //{

    //}

    private Bot bot;
    private Stage stage;

    public CompeteState(Bot bot, Stage stage)
    {
        this.bot = bot;
        this.stage = stage;
    }

    public void OnEnter(Bot bot)
    {
        // Logic when entering the CompeteState
        GoToFinishLine();
    }

    public void OnExecute(Bot bot)
    {
        if (bot.brickCharList.Count == 0)
        {
            bot.ChangeState(new CollectState(stage,bot));
        }
    }

    public void OnExit(Bot bot)
    {
        // Cleanup or exit logic
    }

    private void GoToFinishLine()
    {
        bot.agent.SetDestination(bot.level.finishPoint.position);
    }


}
