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

    private bool censorEnabled = true;
    // Use this for initialization
    void Start ()
    {
        dialogueController = GetComponent<DialogueController>();
        audioManager = GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (censorEnabled)
        {
            CensorCheck();
            if (Input.GetKey("space"))
            {
                staticImage.SetActive(true);
                if (!audioManager.tvStatic.isPlaying)
                    audioManager.tvStatic.Play();
                audioManager.main.mute = true;
            }
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
                        meterController.ChangeThreat(false);
                    else
                        meterController.ChangeThreat(true);
                }
                //else if (Input.GetKey("space"))
                //    meterController.ChangeThreat(true);
            }
        }
        //else if (Input.GetKey("space"))
        //    meterController.ChangeThreat(true);
    }

    public void EnableCensor()
    {
        censorEnabled = true;
    }

    public void DisableCensor()
    {
        censorEnabled = false;
        staticImage.SetActive(false);
        audioManager.tvStatic.Stop();
        audioManager.main.mute = false;
    }
}
