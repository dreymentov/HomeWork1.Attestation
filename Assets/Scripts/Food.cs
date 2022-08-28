using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Food : MonoBehaviour
{
    public TextMeshPro PointsTextFood;

    public int PointFood;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Snake");

        PointFood = Random.Range(1, 100);
        PointsTextFood.SetText(PointFood.ToString());
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

                Debug.Log("Eated food");

                PlayerLenght += PointFood;

                Player.GetComponent<SnakeMovement>().PointsText.SetText(PlayerLenght.ToString());

                Player.GetComponent<SnakeMovement>().Length = PlayerLenght;

                Player.GetComponent<SnakeMovement>().ifNeedCheck = true;

                Destroy(gameObject);
        }
    }
}
