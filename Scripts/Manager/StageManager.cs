using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject[] stageArray = null;
    GameObject currentStage;
    Transform[] stagePlates;

    [SerializeField] float offsetY = -7;
    [SerializeField] float plateSpeed = 5;

    int stepCount = 0;
    int totalPlateCount = 0;

    public void RemoveStage()
    {
        if(currentStage != null)
        {
            Destroy(currentStage);
        }
    }
    public void SettingStage(int p_songNum)
    {
        stepCount = 0;
        currentStage = Instantiate(stageArray[p_songNum], Vector3.zero, Quaternion.identity);
        stagePlates = currentStage.GetComponent<Stage>().plates;
        totalPlateCount = stagePlates.Length;

        for (int i = 0; i < totalPlateCount; i++)
        {
            stagePlates[i].position = new Vector3(stagePlates[i].position.x,
                                                  stagePlates[i].position.y + offsetY,
                                                  stagePlates[i].position.z);
        }
    }

    public void ShowNextPlate()
    {
        if(stepCount < totalPlateCount)
        {
            StartCoroutine(MovePlateCo(stepCount++));
            
        }
       
    }

    IEnumerator MovePlateCo(int i)
    {
        stagePlates[i].gameObject.SetActive(true);
        Vector3 destPos = new Vector3(stagePlates[i].position.x,
                                      stagePlates[i].position.y - offsetY,
                                      stagePlates[i].position.z);
        
        while(Vector3.SqrMagnitude(stagePlates[i].position - destPos) >=0.001f)
        {
            stagePlates[i].position = Vector3.Lerp(stagePlates[i].position, destPos, plateSpeed * Time.deltaTime);
            yield return null;
        }

        stagePlates[i].position = destPos;
    }
}
