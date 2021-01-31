using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Song
{
    public string name;
    public int bpm;
    public Sprite sprite;

}

public class StageMenu : MonoBehaviour
{
    [SerializeField] Song[] songList = null;

    [SerializeField] Text txtSongeName = null;
    [SerializeField] Text txtSongScore = null;
    [SerializeField] Image imgDisk = null;
    [SerializeField] GameObject TitleMenu = null;

    DatabaseManager theDatabase;

    int currentSong = 0;

    void OnEnable()
    {
        if (theDatabase == null)
        {
            theDatabase = FindObjectOfType<DatabaseManager>();
        }
        SettingSong();
        
    }

    public void BtnNext()
    {
        AudioManager.instance.PlaySFX("Touch");
        if (++currentSong > songList.Length-1)
        {
            currentSong = 0;
        }
        SettingSong();
    }

    public void BtnPrior()
    {
        AudioManager.instance.PlaySFX("Touch");
        if (--currentSong < 0)
        {
            currentSong = songList.Length-1;
        }
        SettingSong();
    }

    public void SettingSong()
    {
        txtSongeName.text = songList[currentSong].name;
        imgDisk.sprite = songList[currentSong].sprite;
        txtSongScore.text = string.Format("{0:#,##0}",theDatabase.score[currentSong]);

        AudioManager.instance.PlayBGM("BGM" + currentSong);
    }
    public void BtnBack()
    {
        AudioManager.instance.PlaySFX("Touch");
        TitleMenu.SetActive(true);
        this.gameObject.SetActive(false);
        AudioManager.instance.StopBGM();
    }
    
    public void BtnPlay()
    {

        int t_bpm = songList[currentSong].bpm;

        AudioManager.instance.PlaySFX("Touch");
        GameManager.instance.GameStart(currentSong, t_bpm);
        this.gameObject.SetActive(false);
    }

    public int getSong()
    {
        return currentSong;
    }

    public int getBPM()
    {
        return songList[currentSong].bpm; 
    }
}
