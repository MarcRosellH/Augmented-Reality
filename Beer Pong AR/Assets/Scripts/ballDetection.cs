using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballDetection : MonoBehaviour
{
    public GameObject ball;
    AudioSource audio_source;
    BoxCollider ball_collider;
    private bool audio_start = false;
    public CupManager cupManager;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = this.GetComponent<AudioSource>();
        ball_collider = ball.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audio_start && !audio_source.isPlaying)
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
            if (this.tag == "Player1")
                cupManager.ScoreCup(1);
            else if (this.tag == "Player2")
                cupManager.ScoreCup(2);

            audio_start = false;
        }

    }

    private void OnTriggerEnter(Collider ball)
    {
        if (!audio_start)
        {
            audio_source.Play();
            audio_start = true;
        }
         
    }


}
