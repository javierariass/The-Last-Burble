using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip SoundStage,Boss,Battle,EnemytakeDamage,Write;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Audio.clip = SoundStage;
        Audio.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
