using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsInGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject settings_panel;
    public AudioSource pauseButtonAudioSource;
    public AudioMixer audio_mixer;
    public AudioMixer sfx_mixer;
    public Slider sliderMusic;
    public Slider sliderSFX;
    private float dBMusic;
    private float dBSFX;


    void Start()
    {
        audio_mixer.GetFloat("MusicVolume", out dBMusic);
        sliderMusic.value = Mathf.Pow(10, dBMusic / 20);
        sfx_mixer.GetFloat("SFXVolume", out dBSFX);
        sliderSFX.value = Mathf.Pow(10, dBSFX / 20);

        
        settings_panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSettings()
    {
        if (!settings_panel.active)
        {
            pauseButtonAudioSource.Play();

            settings_panel.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            pauseButtonAudioSource.Play();

            Time.timeScale = 1.0f;
            settings_panel.SetActive(false);
        }
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void SetMusicValue(float sliderValue)
    {
        audio_mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
  
    }

    public void SetSFXValue(float sliderValue)
    {
        sfx_mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
       
    }
}
