using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PresidentController : MonoBehaviour {
    public Animator MouthAnimator;

    public Transform[] Heads;
    public Transform[] Mouths;
    public Transform[] Bodies;

    public bool IsAutoChanging;
    public float HeadAutoChangeTime = 5f;
    public float BodyAutoChangeTime = 5f;

    void Start () {
        //StartTalking();

        if (IsAutoChanging) {
            StartCoroutine(ChangeHeadCoroutine());
            StartCoroutine(ChangeBodyCoroutine());
        }
    }

    public void StartTalking() {
        MouthAnimator.SetTrigger("Talk");
    }

    public void StopTalking() {
        MouthAnimator.SetTrigger("Stop");
    }

    public void ChangeHead() {
        ChangeHead(Random.Range(1,5));
    }

    public void ChangeHead(int target) {
        target--; //adjust for indexing

        for (int i = 0; i < Heads.Length; i++) {
            if (i == target)
            {
                Heads[i].gameObject.SetActive(true);
                Mouths[i].gameObject.SetActive(true);
            }
            else {
                Heads[i].gameObject.SetActive(false);
                Mouths[i].gameObject.SetActive(false);
            }
        }
    }

    public void ChangeBody()
    {
        ChangeBody(Random.Range(1, 5));
    }

    public void ChangeBody(int target) {
        target--; //adjust for indexing

        for (int i = 0; i < Heads.Length; i++) {
            if (i == target)
                Bodies[i].gameObject.SetActive(true);
            else
                Bodies[i].gameObject.SetActive(false);
        }
    }

    private float GetOffset() {
        return Random.Range(0.5f, 1.5f);
    }

    IEnumerator ChangeHeadCoroutine() {
        ChangeHead();
        yield return new WaitForSecondsRealtime(HeadAutoChangeTime + GetOffset());
        yield return StartCoroutine(ChangeHeadCoroutine());
    }

    IEnumerator ChangeBodyCoroutine()
    {
        ChangeBody();
        yield return new WaitForSecondsRealtime(BodyAutoChangeTime + GetOffset());
        yield return StartCoroutine(ChangeBodyCoroutine());
    }
}

