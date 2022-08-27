using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanagerscript : MonoBehaviour
{
    public bool isWinGame;
    public bool isLoseGame;

    public int MaxBlocksInLevel;
    public int MaxPointInThisLevel;

    public GameObject Player;
    public GameObject Apple;


    // Start is called before the first frame update
    void Start()
    {
        isWinGame = false;
        isLoseGame = false;

        MaxBlocksInLevel = LevelIndex + 2;

        Player = GameObject.Find("Snake");
    }

    // Update is called once per frame
    void Update()
    {
        if(isWinGame)
        {
            Debug.Log("Won!");
            LevelIndex++;
            Invoke("GameReload", 2f);
        }

        if (isLoseGame)
        {
            Debug.Log("Game Over!");

            Invoke("GameReload", 2f);
        }
    }

    void GameReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int LevelIndex
    {
        get => PlayerPrefs.GetInt(LevelIndexKey, 0);
        private set
        {
            PlayerPrefs.SetInt(LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }

    private const string LevelIndexKey = "LevelIndex";
}
