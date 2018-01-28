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
    private float signalPoints = 100;

    [SerializeField]
    private float threatPointChange;
    [SerializeField]
    private float signalPointChange;


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
        if (direction)
            signalPoints = Mathf.Clamp(signalPoints + signalPointChange*2, 0, 100);
        else
            signalPoints = Mathf.Clamp(signalPoints - signalPointChange, 0, 100);
        signalRect.sizeDelta = new Vector2((Mathf.Clamp((signalPoints / 100), 0, 100)) * maxWidth, signalRect.rect.height);
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}
