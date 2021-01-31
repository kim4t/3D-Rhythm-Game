using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;



    [SerializeField] Sound[] bgm = null;
    [SerializeField] Sound[] sfx = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;
    

    void Start()
    {
        instance = this;
    }


    public void PlayBGM(string p_bgmName)
    {
        for(int i=0;i<bgm.Length;i++)
        {
            if(bgm[i].name == p_bgmName)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfx[i].name == p_sfxName)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("Every audio players are playing");
                return;
            }
        }
        Debug.Log("There is no audio having such name " + p_sfxName);
    }
}
