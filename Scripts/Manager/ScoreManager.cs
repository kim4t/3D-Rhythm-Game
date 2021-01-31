using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text txtScore = null;
    [SerializeField] int increaseScore = 10;
    int currentScore = 0;
    [SerializeField] float[] weight = null;
    [SerializeField] int comboBonuScore = 10;

    Animator myAnim;
    string scoreUp = "ScoreUp";

    ComboManager theCombo;

    // Start is called before the first frame update
    void Start()
    {
        theCombo = FindObjectOfType<ComboManager>();
        myAnim = GetComponent<Animator>();
        currentScore = 0;
        txtScore.text = "0";
    }

    public void Initialized()
    {
        currentScore = 0;
        txtScore.text = "0";
    }

    public void IncreaseScore(int p_JudgementState)
    {
        // increase combo
        theCombo.IncreaseCombo();
        int currentCombo = theCombo.GetCurrentCombo();
        int bonusComboScore = (currentCombo / 10) * comboBonuScore;

        int score = increaseScore + bonusComboScore;
        score  = (int)(score *(weight[p_JudgementState]));

        currentScore += score;
        txtScore.text = string.Format("{0:#,##0}", currentScore);
        myAnim.SetTrigger(scoreUp);
    }
    
    public int GetCurrentScore()
    {
        return currentScore;
    }
}
