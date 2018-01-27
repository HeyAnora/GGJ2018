using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [Serializable]
    public class Statement
    {
        public AudioClip clip;
        public string transcript;
        public StatementTime[] timings;
    }

    //[Serializable]
    public class StatementTime
    {
        public float intro;
        public float outro;
    }

    [Serializable]
    public class SpeechEvent
    {
        public float delay;
        public UnityEvent action;
    }

    public List<Statement> statementList;

    public SpeechEvent[] eventSchedule;

    public Statement currentStatement;
    private AudioManager audioManager;
    private Censor censor;

    public bool playingStatement = false;

    [SerializeField]
    private Text transcript;

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
        int num = 0; 
        string dialogueText = GetDialogueData();
        string[] list = dialogueText.Split("\n"[0]);

        foreach (string line in list)
        {
            List<StatementTime> DataList = new List<StatementTime>();
            string[] split = line.Split(';');
            foreach (string part in split)
            {
                if (part != "")
                {
                    string[] segment = part.Split('/');
                    float introTime = float.Parse(segment[0].Trim());
                    float outroTime = float.Parse(segment[1].Trim());

                    StatementTime newStatementTime = new StatementTime()
                    {
                        intro = introTime,
                        outro = outroTime
                    };

                    DataList.Add(newStatementTime);
                }

            }
            statementList[num].timings = DataList.ToArray();
            num++;
        }
    }

    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
        speechCoroutine = StartCoroutine(Speech());
        censor = GetComponent<Censor>();
    }

    public void PlayStatement(int num)
    {
        playingStatement = true;
        currentStatement = statementList[num];
        audioManager.PlaceSpeech(currentStatement.clip);
        transcript.text = currentStatement.transcript;
    }

    public void PlayApplause()
    {
        currentStatement.clip = null;
        audioManager.PlayApplause();
    }

    private Coroutine speechCoroutine;
    private IEnumerator Speech()
    {
        for (int i = 0; i < eventSchedule.Length; i++)
        {
            yield return new WaitForSeconds(eventSchedule[i].delay);
            eventSchedule[i].action.Invoke();
            while (audioManager.main.isPlaying)
                yield return null;
            playingStatement = false;
        }
    }
}
