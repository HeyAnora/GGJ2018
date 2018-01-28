using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterController : MonoBehaviour
{
    [SerializeField]
    private RectTransform signalRect;
    [SerializeField]
    private RectTransform threatRect;

    private float maxWidth = 424f;

    private float threatPoints = 0;
    [SerializeField]
    private float signalPoints = 100;

    [SerializeField]
    private float threatPointChange;
    [SerializeField]
    private float signalPointChange;

    [SerializeField]
    private Censor censor;
    private bool cooldownEnabled = false;

	// Use this for initialization
	void Start ()
    {
        signalRect.sizeDelta = new Vector2(424, signalRect.rect.height);
        threatRect.sizeDelta = new Vector2(0, threatRect.rect.height);
	}
	

    public void ChangeThreat(bool direction)
    {
        if(direction)
            threatPoints = Mathf.Clamp( threatPoints + threatPointChange,0,100);
        else
            threatPoints = Mathf.Clamp(threatPoints - threatPointChange, 0, 100);

        threatRect.sizeDelta = new Vector2((Mathf.Clamp((threatPoints/100),0,100))*maxWidth, threatRect.rect.height);
    }

    public void ChangeSignal(bool direction)
    {
        if (!cooldownEnabled)
        {
            if (direction)
                signalPoints = Mathf.Clamp(signalPoints + signalPointChange / 2, 0, 100);
            else
                signalPoints = Mathf.Clamp(signalPoints - signalPointChange, 0, 100);
            signalRect.sizeDelta = new Vector2((Mathf.Clamp((signalPoints / 100), 0, 100)) * maxWidth, signalRect.rect.height);
        }
        if (signalPoints <=0)
            cooldownRoutine = StartCoroutine(Cooldown());
    }

    private Coroutine cooldownRoutine;
    private IEnumerator Cooldown()
    {
        cooldownEnabled = true;
        censor.DisableCensor();
        while (signalPoints < 100)
        {
            signalPoints = Mathf.Clamp(signalPoints + signalPointChange / 4, 0, 100);
            signalRect.sizeDelta = new Vector2((Mathf.Clamp((signalPoints / 100), 0, 100)) * maxWidth, signalRect.rect.height);
            yield return null;
        }
        censor.EnableCensor();
        cooldownEnabled = false;
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("space"))
        {
            ChangeSignal(false);
        }
        else
            ChangeSignal(true);
	}
}
