using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    // Start is called before the first frame update
    public bool plankCollision;

    AudioSource audio_source;

    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="ball")
        {
           plankCollision = true;
        }
    }
}
