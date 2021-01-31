using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] goGameUI = null;
    [SerializeField] GameObject goTitleUI = null;

    [SerializeField] Material[] skyBoxArr = null;
   

    public static GameManager instance;

    public bool isStartGame = false;

    ComboManager theCombo;
    ScoreManager theScore;
    TimingManager theTiming;
    StatusManager theStatus;
    PlayerController thePlayer;
    StageManager theStage;
    NoteManager theNoteManager;
    Result theResult;
    [SerializeField] CenterFrame theMusic = null;
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theResult = FindObjectOfType<Result>();
        theStage = FindObjectOfType<StageManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theScore = FindObjectOfType<ScoreManager>();
        theTiming = FindObjectOfType<TimingManager>();
        theStatus = FindObjectOfType<StatusManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        theNoteManager = FindObjectOfType<NoteManager>();
        
    }

   
    public void GameStart(int p_songNum, int p_bpm)
    {
        theMusic.ResetMusic();
       
        for (int i=0;  i<goGameUI.Length;i++)
        {
            goGameUI[i].SetActive(true);
        }
        theMusic.bgmName = "BGM"+p_songNum;
        theNoteManager.noteBpm = p_bpm;
        theStage.RemoveStage();
        theStage.SettingStage(p_songNum);
        theCombo.ResetCombo();
        thePlayer.Initialized();
        theStatus.Initialized();
        theTiming.Initialized();
        theScore.Initialized();
        theResult.SetCurrentSong(p_songNum, p_bpm);

        RenderSettings.skybox = skyBoxArr[p_songNum];

        AudioManager.instance.StopBGM();
        isStartGame = true;
       
    }

    public void MainMenu()
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(false);
        }
        goTitleUI.SetActive(true);
    }
}
