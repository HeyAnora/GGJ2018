using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Censor : MonoBehaviour
{
    private DialogueController dialogueController;
    private AudioManager audioManager;
    [SerializeField]
    private MeterController meterController;

    [SerializeField]
    private GameObject staticImage;
    // Use this for initialization
    void Start ()
    {
        dialogueController = GetComponent<DialogueController>();
        audioManager = GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CensorCheck();
        if (Input.GetKey("space"))
        {
            staticImage.SetActive(true);
            if (!audioManager.tvStatic.isPlaying)
                audioManager.tvStatic.Play();
            audioManager.main.mute = true;
        }
        if (Input.GetKeyUp("space"))
        {
            staticImage.SetActive(false);
            audioManager.tvStatic.Stop();
            audioManager.main.mute = false;
        }

    }

    private void CensorCheck()
    {
        if (dialogueController.playingStatement == true)
        {
            foreach (DialogueController.StatementTime timing in dialogueController.currentStatement.timings)
            {
                if (audioManager.main.time > timing.intro && audioManager.main.time < timing.outro)
                {
                    if (Input.GetKey("space"))
                        AddPoints(true);
                    else
                        AddPoints( false);
                }
                else if (Input.GetKey("space"))
                    AddPoints(false);
            }
        }
        else if (Input.GetKey("space"))
            AddPoints(false);
    }

    private void AddPoints(bool check)
    {
        if (check)
            Debug.Log(audioManager.main.time + "Points++");
        else
            Debug.Log(audioManager.main.time + "Points--");
    }
}
