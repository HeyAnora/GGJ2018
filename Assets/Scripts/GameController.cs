using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    private AudioManager audioManager;
    private DialogueController dialogueController;

	// Use this for initialization
	void Start ()
    {
        audioManager = GetComponent<AudioManager>();
        dialogueController = GetComponent<DialogueController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
