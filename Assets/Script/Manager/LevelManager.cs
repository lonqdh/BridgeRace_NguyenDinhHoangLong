using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Bot botPrefab;
    [NonSerialized] public Player player;
    [NonSerialized] public List<Bot> bots = new List<Bot>();
    private List<ColorType> availableColors = new List<ColorType>();
    public FloatingJoystick joystick;

    Level currentLevel;
    int level = 1;

    private void Start()
    {
        availableColors.Add(ColorType.Green);
        availableColors.Add(ColorType.Blue);
        availableColors.Add(ColorType.Red);
        availableColors.Add(ColorType.Yellow);

        LoadLevel();
    }

    public void LoadLevel()
    {
        LoadLevel(level);
    }

    public void LoadLevel(int levelIndex)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[levelIndex - 1]);

        // Create a list of available colors to shuffle
        //List<ColorType> availableColorsCopy = new List<ColorType>(Stage.Instance.availableColors);

        // Shuffle the colors to make them random
        ShuffleList(availableColors);
        // Assign colors to player and bots
        player = Instantiate(playerPrefab);
        player.colorType = availableColors[0];
        player.GetComponent<ColorGameObject>().ChangeColor(player.colorType);
        player.transform.position = currentLevel.startPointList[0].position;
        player.OnInit();
        Stage.Instance.availableChar.Add(player);

        for (int i = 0; i < 3; i++)
        {
            Bot bot = Instantiate(botPrefab);
            bots.Add(bot);
        }
        for (int i = 0; i < 3; i++)
        {
            bots[i].colorType = availableColors[i + 1];
            bots[i].GetComponent<ColorGameObject>().ChangeColor(bots[i].colorType);
            Stage.Instance.availableChar.Add(bots[i]);
        }


        for (int i = 0; i < 3; i++)
        {
            bots[i].transform.position = currentLevel.startPointList[i + 1].position;
        }

    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
