using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Censor : MonoBehaviour
{

    private void ActivateCensor()
    {
        Debug.Log("Censoring");
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("space"))
        { 
            ActivateCensor();
        }
	}
}
