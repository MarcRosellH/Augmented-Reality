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
    private AudioSource audioSource;
   

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
      
    }

    public void PlayGame()
    {
        audioSource.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void  OpenSettings()
    {
        audioSource.Play();
        mainPanel.SetActive(false);
        panelSettings.SetActive(true);
    }

    public void CloseSettings()
    {
        audioSource.Play();
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
        audioSource.Play();
        Application.Quit();
    }
}
