using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuFuncs : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelSettings;
    public GameObject mainPanel;
    public AudioMixer audio_mixer;
    public AudioMixer sfx_mixer;
   

    void Start()
    {
 

    }

    void Update()
    {
      
    }

    public void PlayGame()
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

    public void SetMusicValue(float sliderValue)
    {
        audio_mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        //float db;
        //audio_mixer.GetFloat("MusicVolume", out db);
        //PlayerPrefs.SetFloat("MusicVolume", db);
    }

    public void SetSFXValue(float sliderValue)
    {
        sfx_mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        //float db;
        //sfx_mixer.GetFloat("SFXVolume", out db);
        //PlayerPrefs.SetFloat("SFXVolume", db);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
