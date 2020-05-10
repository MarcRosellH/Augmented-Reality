using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int numberCups = 9;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(numberCups == 0)
        {
            Debug.Log("Finished game");
        }
    }

    public void ScoreCup()
    {
        numberCups--;
    }
}
