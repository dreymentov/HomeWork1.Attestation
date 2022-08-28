using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public TextMeshPro PointsTextBlock;

    public int PointBlock;

    public GameObject GameManager;

    public GameObject BlockGO;

    public GameObject Player;

    public ParticleSystem PS;

    public Material material;

    public float gradient;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        Player = GameObject.Find("Snake");
        BlockGO = this.gameObject;
        PointBlock = Random.Range(1, 100);
        PointsTextBlock.SetText(PointBlock.ToString());
        gradient = (float)PointBlock / 100;
        Debug.Log(this.gameObject + " gradient: " + gradient);

        BlockGO.GetComponent<Renderer>().material.SetFloat("Gradient1", gradient);
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
                GameObject PSinst = Instantiate(PS.gameObject);
                PSinst.transform.position = this.gameObject.transform.position;
                Destroy(PSinst, 2f);
                Destroy(gameObject);
                GameManager.gameObject.GetComponent<Gamemanagerscript>().blockCrashed++;
            }
            else if(PlayerLenght <= PointBlock)
            {
                Destroy(Player);
                Debug.Log("Game Over!");
                Invoke("lose", 2f);
            }
        }
    }

    void lose()
    {
        Debug.Log("Invoked lose");
        GameManager.GetComponent<Gamemanagerscript>().RestartGame();
    }
}
