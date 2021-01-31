using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFrame : MonoBehaviour
{

    bool musicStart = false;

    public string bgmName = "";

    public bool isStart()
    {
        return musicStart;
    }
    
    public void ResetMusic()
    { 
        musicStart = false;
        Debug.Log("Reset " + musicStart);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (!musicStart)
        {
            if (collision.CompareTag("Note"))
            {
               
                AudioManager.instance.PlayBGM(bgmName);

                musicStart = true;
            }
        }


    }

}
