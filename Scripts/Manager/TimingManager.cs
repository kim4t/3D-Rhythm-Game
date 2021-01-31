using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    int[] judgementRecord = new int[5];


    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null;

    EffectManager theEffect;
    ScoreManager theScoreManager;
    ComboManager theCombo;
    StageManager theStageManger;
    PlayerController thePlayerController;
    StatusManager theStatusManager;
    //AudioManager theAudioManger;

    // Start is called before the first frame update
    void Start()
    {
        //theAudioManger = AudioManager.instance;
        //theAudioManger = FindObjectOfType<AudioManager>();
        theStatusManager = FindObjectOfType<StatusManager>();
        thePlayerController = FindObjectOfType<PlayerController>();
        theStageManger = FindObjectOfType<StageManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();

        timingBoxs = new Vector2[timingRect.Length];
        
        for (int i = 0; i < timingRect.Length; i++)
        {
            
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
            //Debug.Log("timingbox"+i+" range = "+timingBoxs[i].x+"  "+timingBoxs[i].y);
        }
        
    }

    public bool CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.y;

            for (int x = 0; x < timingBoxs.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    if(boxNoteList[i].CompareTag("Note"))
                    {
                        boxNoteList[i].GetComponent<Note>().HideNote();

                        if(x < timingBoxs.Length-1)
                        {
                            theEffect.NoteHitEffectOne();
                        }
                        boxNoteList.RemoveAt(i);
                        
                        if(CheckCanNextPlate())
                        {
                            theStageManger.ShowNextPlate();
                            // Increase score
                            theScoreManager.IncreaseScore(x);
                            theEffect.JudementEffect(x);

                            judgementRecord[x]++;
                            theStatusManager.checkShield();
                        }
                        else
                        {
                            theEffect.JudementEffect(5);
                            
                        }

                        //theAudioManger.PlaySFX("Clap");
                        //theAudioManger.PlaySFX("Clap");
                        AudioManager.instance.PlaySFX("Clap");


                        return true;
                    }
                    
  
                }
            }
        }
        
        theCombo.ResetCombo();
        theEffect.JudementEffect(4);
        MissRecord();
        return false;

    }

    bool CheckCanNextPlate()
    {
        if(Physics.Raycast(thePlayerController.destPos, Vector3.down, out RaycastHit t_hitInfo, 1.1f))
        {
            if(t_hitInfo.transform.CompareTag("BasicPlate"))
            {
                BasicPlate t_plate = t_hitInfo.transform.GetComponent<BasicPlate>();
                if(t_plate.flag)
                {
                    t_plate.flag = false;
                    return true;
                }
            }
        }
        return false;
    }
   
    public int[] GetJudgementRecord()
    {
        return judgementRecord;
    }

    public void MissRecord()
    {
        judgementRecord[4]++;
        theStatusManager.ResetShieldCombo();
    }

    public void Initialized()
    {
        for (int i =0;i<judgementRecord.Length;i++)
        {
            judgementRecord[i] = 0;
        }
    }
}
