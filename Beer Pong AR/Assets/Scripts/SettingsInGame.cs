using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsInGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject settings_panel;
    public AudioClip open_menu;
    AudioSource audio_source;

    void Start()
    {
        audio_source = GetComponent<AudioSource>();
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
            audio_source.Stop();
            audio_source.clip = open_menu;
            audio_source.Play();

            settings_panel.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            audio_source.Stop();
            audio_source.clip = open_menu;
            audio_source.Play();

            Time.timeScale = 1.0f;
            settings_panel.SetActive(false);
        }
    }
}
