using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource main;

    [SerializeField]
    private AudioClip applause;

    public AudioSource tvStatic;

    public void PlaceSpeech(AudioClip clip)
    {
        main.clip = clip;
        main.volume = 1;
        main.Play();
    }

    public void PlayApplause()
    {
        main.clip = applause;
        main.volume = .1f;
        main.Play();
    }

	// Use this for initialization
	void Start ()
    {
		
	}
}
