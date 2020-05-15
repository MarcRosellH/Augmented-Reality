using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFuncs : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelSettings;
    public GameObject mainPanel;
    public Slider musicSlider;
    public AudioSource audioSource;
    private float volume;
    void Start()
    {
        volume = audioSource.volume;
        audioSource.volume = volume * musicSlider.value;

    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void  OpenSettings()
    {
        mainPanel.SetActive(false);
        panelSettings.SetActive(true);
    }

    public void CloseSettings()
    {
        panelSettings.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ChangeMusicValue()
    {
        audioSource.volume = volume * musicSlider.value;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
