using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator1 = null;
    [SerializeField] Animator noteHitAnimator2 = null;
    [SerializeField] Animator noteHitAnimator3 = null;

    [SerializeField] Animator judgementAnimator = null;
    [SerializeField] UnityEngine.UI.Image judgementImage = null;
    [SerializeField] Sprite[] judgementSprite = null;

    string hit = "Hit";
    string hit1 = "Hit1";
    string hit2 = "Hit2";
    string hit3 = "Hit3";

    public void NoteHitEffectOne()
    {
        noteHitAnimator1.SetTrigger(hit1);
    }
    public void NoteHitEffectTwo()
    {
        noteHitAnimator2.SetTrigger(hit2);
    }
    public void NoteHitEffectThree()
    {
        noteHitAnimator3.SetTrigger(hit3);
    }


    public void JudementEffect(int i)
    {
        judgementImage.sprite = judgementSprite[i];
        judgementAnimator.SetTrigger(hit);
    }
}
