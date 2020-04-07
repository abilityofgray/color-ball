using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{

    public int LevelIndex;
    public bool levelComplete = false;
    public Vector3 playerPositionAtStart;
    public PointTextUI pointTextUI;
    public float WinPointPercent = 1; //default
    public int LampCountOnLevel;
    public List<Lamp> levelLamps;

    public void LevelInit() {

        levelLamps = new List<Lamp>();

        LevelLampsInit();



    }

    public void LevelLampsInit() {

        for (int i = 0; i < transform.childCount; i++)
        {

            
            if (transform.GetChild(i).TryGetComponent<Lamp>(out Lamp lamp))
            {

                lamp.LampInit();
                levelLamps.Add(lamp);
                
            }

        }
        //Debug.Log("LevelLampInit: " + transform.childCount);
    }

    public void SaveStartPlayerPosition() {

        Vector3 startPlatform = GetComponentInChildren<PlatformStartLevel>().transform.localPosition;

        playerPositionAtStart = new Vector3(startPlatform.x, startPlatform.y + 25f, 5);
        
    }



}
