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
    throwObject _thrownObject;
    private float timeWhenDisappear = 5.0f;
    private float timeToAppear = 3.0f;
    private bool fadingOut = false;
    void Start()
    {
        player2.SetActive(false);
        player1.SetActive(true);
        turnText.enabled = false;

        _thrownObject = this.GetComponent<throwObject>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(player1.active)
            {
                _thrownObject.playerTurn = 1;


                _thrownObject.ChangeTurnReset();
            }
            else
            {
                _thrownObject.playerTurn = 2;


                _thrownObject.ChangeTurnReset();

            }
        }
        if (turnText.enabled && (Time.time >= timeWhenDisappear))
        {
            turnText.enabled = false;
        }


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
        EnableText();

       
     
    }

    public void EnableText()
    {
        turnText.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    }
}
