using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public Player player;
    //public Bot bot;
    Level currentLevel;

    int level = 1;

    private void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        LoadLevel(level);
        OnInit();
    }

    public void LoadLevel(int levelIndex)
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[levelIndex - 1]);
    }

    public void OnInit()
    {
        //bot.transform.position = currentLevel.startPoint.position;
        player.transform.position = currentLevel.startPoint.position;
        player.OnInit();
        //bot.OnInit();
    }


}
