using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int noteBpm = 0;
    double currentTime = 0d;
   
   
    

    [SerializeField] Transform tfNoteAppear = null;
    
    [SerializeField] GameObject goNote = null;


    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager theCombo;

    void Start()
    {
        
        theTimingManager = GetComponent<TimingManager>();
        theEffectManager = FindObjectOfType<EffectManager>();
        theCombo = FindObjectOfType<ComboManager>();
    }

    void Update()
    {
        if(GameManager.instance.isStartGame)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 60d / noteBpm)
            {
                GameObject t_note1 = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
                
                

                RectTransform note1Rotate = t_note1.GetComponent<RectTransform>();
                note1Rotate.Rotate(new Vector3(0, 0, 90));
                t_note1.transform.SetParent(this.transform);

                theTimingManager.boxNoteList.Add(t_note1);

                currentTime -= 60d / noteBpm;
            }
        }   

      }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Note"))
        {
            if(collision.GetComponent<Note>().GetNoteFlag())
            {
                theTimingManager.MissRecord();
                theEffectManager.JudementEffect(4);
                theCombo.ResetCombo();
            }
          
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
        
    }
    public void RemoveNote()
    {
        GameManager.instance.isStartGame = false;

        for (int i=0;i<theTimingManager.boxNoteList.Count; i++)
        {
            theTimingManager.boxNoteList[i].SetActive(false);
        }

        theTimingManager.boxNoteList.Clear();
    }
}
