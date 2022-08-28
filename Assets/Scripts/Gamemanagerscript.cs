using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanagerscript : MonoBehaviour
{
    public bool isWinGame;
    public bool isLoseGame;
    public bool isGaming;

    public int MaxBlocksInLevel;
    public int MaxPointInThisLevel;
    public int durationedGame;
    public int blockCrashed;

    public GameObject Player;
    public GameObject Apple;
    public GameObject Blocks;
    public GameObject UIInfo;
    public GameObject UIWin;
    public GameObject UILose;

    public float TimeForLevelGame;
    public float minRangeX = -2.5f;
    public float maxRangeX = 2.5f;
    public float durationGame;
    public float randomPosX;
    public float posYforInst;
    public float maxYbeforePlayer;

    // Start is called before the first frame update
    void Start()
    {
        isWinGame = false;
        isLoseGame = false;
        isGaming = true;

        blockCrashed = 0;

        Player = GameObject.Find("Snake");

        Debug.Log("Level:" + LevelIndex);

        MaxBlocksInLevel = LevelIndex + 1;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            blockCrashed++;


        if(isGaming)
        {
            UIInfo.SetActive(true);
            UIWin.SetActive(false);
            UILose.SetActive(false);

            durationGame += Time.deltaTime;

            posYforInst = Player.transform.position.y;

            if (durationGame > 5)
            {
                durationGame = 0;
                durationedGame++;
                RandomRangeLevel(randomPosX);

                if (durationedGame % 2 == 0)
                {
                    Instantiate(Blocks, new Vector3(0, posYforInst + maxYbeforePlayer, 0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(Apple, new Vector3(randomPosX, posYforInst + maxYbeforePlayer, 0f), Quaternion.identity);
                }
            }

            if (blockCrashed >= MaxBlocksInLevel)
            {
                Debug.Log("Won!");
                Invoke("WinGame", 2f);
            }
        }

        if(isWinGame)
        {
            UIInfo.SetActive(false);
            UIWin.SetActive(true);
        }

        if(isLoseGame)
        {
            UIInfo.SetActive(false);
            UILose.SetActive(true);
        }

        if(!Player)
        {
            isLoseGame = true;
        }
    }

    public void GameReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        LevelIndex++;
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

    void RandomRangeLevel(float random)
    {
        random = Random.Range(minRangeX, maxRangeX);
    }

    public void WinGame()
    {
        isWinGame = true;
        isGaming = false;
    }

    public void RestartGame()
    {
        isLoseGame = true;
        isGaming = false;
    }
}
