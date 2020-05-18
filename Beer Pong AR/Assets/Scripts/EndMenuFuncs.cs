using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuFuncs : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource buttonClickSFX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RestartGame()
    {
        buttonClickSFX.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExitGame()
    {
        buttonClickSFX.Play();
        Application.Quit();
    }
}
