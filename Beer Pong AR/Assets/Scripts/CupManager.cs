using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberCups1 = 9;
    public int numberCups2 = 9;
    public Text counterPlayer1;
    public Text counterPlayer2;

    void Start()
    {
        counterPlayer1.text = "Player 1: " + numberCups1;
        counterPlayer2.text = "Player 2: " + numberCups2;
    }

    // Update is called once per frame
    void Update()
    {
        if(numberCups1 == 0 || numberCups2 == 0)
        {
            Debug.Log("Finished game");
        }
    }

    public void ScoreCup(int player)
    {
        if (player == 1)
        {
            numberCups1--;
            counterPlayer1.text = "Player 1: " + numberCups1;
            
        }
        else
        {
            numberCups2--;
            counterPlayer2.text = "Player 2: " + numberCups2;
        }
        
    }
}
