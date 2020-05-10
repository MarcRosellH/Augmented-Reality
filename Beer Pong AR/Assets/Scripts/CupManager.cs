using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CupManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberCups1 = 9;
    public int numberCups2 = 9;
    public Text counterPlayer1;
    public Text counterPlayer2;
    public TextMeshProUGUI whoWin;
    public GameObject panelContinue;

    void Start()
    {
        counterPlayer1.text = "Player 1: " + numberCups1;
        counterPlayer2.text = "Player 2: " + numberCups2;
        panelContinue.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            numberCups1 = 0;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            ScoreCup(1);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ScoreCup(2);
        }

        if (numberCups1 == 0 || numberCups2 == 0)
        {
            Debug.Log("Finished game");
            Time.timeScale = 0.0f;

            if (numberCups1 == 0)
                whoWin.text = "Player 2 wins!";
            else
                whoWin.text = "Player 1 wins!";

            panelContinue.SetActive(true);
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
