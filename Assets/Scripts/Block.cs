using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public TextMeshPro PointsTextBlock;

    public int PointBlock;

    public GameObject GameManager;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        Player = GameObject.Find("Snake");

        PointBlock = Random.Range(1, 100);
        PointsTextBlock.SetText(PointBlock.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Player)
        {
            int PlayerLenght = Player.GetComponent<SnakeMovement>().Length;

            if (PlayerLenght > PointBlock)
            {
                Debug.Log("Crashed block");
                PlayerLenght -= PointBlock;
                Player.GetComponent<SnakeMovement>().PointsText.SetText(PlayerLenght.ToString());
                Player.GetComponent<SnakeMovement>().Length = PlayerLenght;
                Player.GetComponent<SnakeMovement>().ifNeedCheck = true;
                Destroy(gameObject);
            }
            else if(PlayerLenght <= PointBlock)
            {
                Destroy(Player);
                GameManager.gameObject.GetComponent<Gamemanagerscript>().isLoseGame = true;
            }
        }
    }
}
