using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    //[Serializable]
    public class StatementTime
    {
        public float intro;
        public float outro;
    }

    public List<StatementTime> DataList;

    private void Awake()
    {
        InitiateData();
    }

    private static string GetDialogueData()
    {
        TextAsset file = Resources.Load("DialogueData") as TextAsset;
        return file.text;
    }

    private void InitiateData()
    {
        string dialogueText = GetDialogueData();
        string[] list = dialogueText.Split("\n"[0]);

        foreach (string line in list)
        {
            string[] split = line.Split('/');
            float introTime = float.Parse(split[0].Trim());
            float outroTime = float.Parse(split[1].Trim());

            StatementTime newStatement = new StatementTime()
            {
                intro = introTime,
                outro = outroTime
            };

            DataList.Add(newStatement);
        }

    }

    ///TO DO: 
    ///add way to check list timings
    ///check for each set of timings at a time, after one is checked begin chekcing next one


}
