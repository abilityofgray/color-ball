using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UserPointData : ScriptableObject
{

    [SerializeField]
    private int   currentPoint;
    [SerializeField]
    private int   recordPoint;
    [SerializeField]
    private int    prevLevelComplete;
    [SerializeField]
    private int     levelStart;

    public int CurrentPoint { set { currentPoint = value; } get { return currentPoint; } }
    public int RecordPoint { set { recordPoint = value; } get { return recordPoint; } }
    public int PrevLevelComplete { set { prevLevelComplete = value; } get { return prevLevelComplete; } }
    public int LevelStart { set { levelStart = value; } get { return levelStart; } }


    private void OnEnable()
    {

        //Debug.Log("UserPointReady");

        //TODO: make properties string field 
        //currentPoint = PlayerPrefs.GetInt("CurrentPoint");
        levelStart = PlayerPrefs.GetInt("LevelStart", prevLevelComplete);
        recordPoint = PlayerPrefs.GetInt("RecordPoint");

    }

    private void OnDisable()
    {

        //Debug.Log("UserData Saved");

        //TODO: make properties string field 
        //PlayerPrefs.SetInt("CurrentPoint", currentPoint);
        PlayerPrefs.SetInt("LevelStart", levelStart);
        PlayerPrefs.SetInt("RecordPoint", recordPoint);
        PlayerPrefs.SetInt("CurrentLevel", levelStart);

    }

}
