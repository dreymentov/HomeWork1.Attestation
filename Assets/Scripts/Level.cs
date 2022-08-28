using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public GameObject GameManager;
    public Text Textlevel;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        Textlevel.text = "LEVEL " + (GameManager.GetComponent<Gamemanagerscript>().LevelIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
