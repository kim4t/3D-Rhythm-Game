using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [SerializeField] float blinkSpeed = 0.1f;
    [SerializeField] int blinkCount = 10;
    int currentBlinkCount = 0;

    bool isBlink = false;
    bool isDead = false;
    int maxHp = 3;
    int currentHp = 3;

    int maxShield = 3;
    int currentShield = 0;

    [SerializeField] Image[] hpImage = null;
    [SerializeField] Image[] shieldImage = null;
    [SerializeField] Image shieldGauge = null;


    [SerializeField] int shieldIncreaseCombo = 5;
    int currentShieldCombo = 0;
    

    Result theResult;
    NoteManager theNoteManager;
    [SerializeField] MeshRenderer playerMesh = null;


    void Start()
    {
        theResult = FindObjectOfType<Result>();
        theNoteManager = FindObjectOfType<NoteManager>();
    
    }

    public void checkShield()
    {
        currentShieldCombo += 1;
        if(currentShieldCombo >= shieldIncreaseCombo)
        {
            currentShieldCombo = 0;
            increaseShield();
        }
        shieldGauge.fillAmount = (float)currentShieldCombo / shieldIncreaseCombo;
    }

    public void ResetShieldCombo()
    {
        currentShieldCombo = 0;
        shieldGauge.fillAmount = (float)currentShieldCombo / shieldIncreaseCombo;
    }

    public void increaseShield()
    {
        currentShield += 1;
        if(currentShield >= maxShield)
        {
            currentShield = maxShield;
        }

        SettingShieldImage();
    }

    public void decreaseShield(int i)
    {
        currentShield -= 1;
        
        if (currentShield <=0)
        {
            currentShield = 0;
        }

        SettingShieldImage();
    }


    public void IncreseHp(int i)
    {
        currentHp += i;
        if(currentHp >= maxHp)
        {
            currentHp = maxHp;
        }
        SettingHPImage();
    }

    public void DecreaseHp(int i)
    {
        if(!isBlink)
        {
            if(currentShield >0)
            {
                decreaseShield(1);
            }
            else
            {
                currentHp -= i;

                if (currentHp <= 0)
                {
                    isDead = true;
                    theResult.ShowResult();
                    theNoteManager.RemoveNote();
                }
                else
                {
                    StartCoroutine(BlinkCo());
                }
                SettingHPImage();
            }
            
        }
        
    }

    void SettingHPImage()
    {
        for( int i=0;i<hpImage.Length;i++)
        {
            if(i< currentHp)
            {
                hpImage[i].gameObject.SetActive(true);
            }
            else
            {
                hpImage[i].gameObject.SetActive(false);
            }
        }
    }

    void SettingShieldImage()
    {
        for (int i = 0; i < shieldImage.Length; i++)
        {
            if (i < currentShield)
            {
                shieldImage[i].gameObject.SetActive(true);
            }
            else
            {
                shieldImage[i].gameObject.SetActive(false);
            }
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    IEnumerator BlinkCo()
    {
        isBlink = true;
        while(currentBlinkCount <= blinkCount)
        {
            playerMesh.enabled  =! playerMesh.enabled;
            yield return new WaitForSeconds(blinkSpeed);
            currentBlinkCount += 1;
        }
        playerMesh.enabled = true;
        isBlink = false;
        currentBlinkCount = 0;
    }

    public void Initialized()
    {
        currentHp = maxHp;
        currentShield = 0;
        currentShieldCombo = 0;
        shieldGauge.fillAmount = 0;
        isDead = false;
        SettingHPImage();
        SettingShieldImage();
    }
}
