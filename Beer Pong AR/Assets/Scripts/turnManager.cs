using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player1;
    public GameObject player2;
    public Text turnText;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTurn(int turn)
    {
        if (turn == 1)
        {
            player2.SetActive(false);
            player1.SetActive(true);
        }
        else
        {
            player1.SetActive(false);
            player2.SetActive(true);
        }

        turnText.text = "Player " + turn + " turn";
    }
}
