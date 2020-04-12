using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsController : MonoBehaviour
{
    public static LevelsController instance = null;




    public Dictionary<int, Level> levelsDict = new Dictionary<int, Level>();

    public Level[] levelsRawData;


    bool levelExpire = false;

    int previouseLevel;
    private int currentLevel;
    private int startAtLevel;
    private int allPointAtCurrentLevel;
    private float winPercentCurrentLevel = 0;


    public int GetCurrentLevel { get { return currentLevel; } }
    public int StartAtLevel { get { return startAtLevel; } set { startAtLevel = value; } }
    public int AllPointAtCurrentLevel { get { return allPointAtCurrentLevel; } }
    public float GetWinPercentCurrentLevel { get { return winPercentCurrentLevel; } }

    public LampBrokenParticle lampBrokenParticleReference;

    private void Start()
    {

        if (instance == null) instance = this;

        //levelsRawData = FindObjectsOfType<Level>();
        if (levelsRawData.Length <= 1) {

            Debug.Log("<color=red>Check if all levels GO is enabled!!!</color>");

        }
        AddFindLevelsToDictionary();
        //StartFirstLevel(1);
        
        StartLevel(StartAtLevel);



    }

    void AddFindLevelsToDictionary() {



        for (int i = 0; i < levelsRawData.Length; i++) {

            levelsRawData[i].SaveStartPlayerPosition();
            //levelsRawData[i].gameObject.SetActive(false);
            levelsDict.Add(levelsRawData[i].LevelIndex, levelsRawData[i]);

        }

        
        foreach (KeyValuePair<int, Level> kvp in levelsDict) {

            //kvp.Value.SaveStartPlayerPosition();
            //kvp.Value.gameObject.SetActive(false);
            //Debug.Log(kvp.Key + " : " + kvp.Value);

        }
        
    }

    public void StartLevel(int index) {

        //Temporary sheme
        
        //lampBrokenParticleReference = FindObjectOfType<LampBrokenParticle>();
        
        //MainUI.instance.StartScreenEnabled();
        if (levelsDict.ContainsKey(index)) {

            currentLevel = index;

            levelsDict[index].LevelInit();
            levelsDict[index].gameObject.SetActive(true);

            GetAllPointCountAtLevel();

            //TODO: replace to Lamp.cs
            GetCurrentLevelWinPercent(index);

            GameController.instance.ResetPointCollectAtCurrentLevel();
            GameController.instance.GetGameAnalytic.StartLevel(index);

            Vector3 playerPos = new Vector3(levelsDict[index].playerPositionAtStart.x, levelsDict[index].playerPositionAtStart.y, 0);
            FindObjectOfType<Player>().transform.position = playerPos;

            InitLevelReferenceLampBrokeParticle(index);

            
        }

    }
    /// <summary>
    /// temporary needs sheme
    /// </summary>
    public void InitLevelReferenceLampBrokeParticle(int levelIndex) {
        
        for (int i = 0; i < levelsDict[levelIndex].levelLamps.Count; i++)
        {

            levelsDict[levelIndex].levelLamps[i].LampBrokeParticle = lampBrokenParticleReference;

        }

    }

    public void GetCurrentLevelStartPlayerPosition() {

        if (levelsDict.ContainsKey(currentLevel))
        {

            Vector3 playerPos = new Vector3(levelsDict[currentLevel].playerPositionAtStart.x, levelsDict[currentLevel].playerPositionAtStart.y, 0);
            FindObjectOfType<Player>().GetComponent<Rigidbody>().velocity = Vector3.zero;
            FindObjectOfType<Player>().transform.position = playerPos;

        }

    }

    public void GetAllPointCountAtLevel() {

        Lamp[] lamps = FindObjectsOfType<Lamp>();
        //Platform[] platforms = FindObjectsOfType<Platform>();

        for (int i = 0; i < lamps.Length; i++)
        {

            if (lamps[i].isDetroyed && !lamps[i].PlatformNotGainPoint)
            {

                allPointAtCurrentLevel += lamps[i].PointCost * lamps[i].Lamps.Count;

            }
            else if (!lamps[i].isDetroyed && !lamps[i].PlatformNotGainPoint) {

                allPointAtCurrentLevel += lamps[i].PointCost;

            }

        }

    }

    void GetCurrentLevelWinPercent(int levelIndex) {

        winPercentCurrentLevel = levelsDict[levelIndex].WinPointPercent;
        
    }

    void GetCurrentLevelWinPoint(int levelIndex) {

        winPercentCurrentLevel = levelsDict[levelIndex].WinPointPercent;


    }

    public void NextLevelStart() {



        if (currentLevel <= levelsDict.Count && !levelExpire)
        {

            //MainUI.instance.StartScreenEnabled();

            previouseLevel = currentLevel;

            GameController.instance.GetGameAnalytic.LevelComplete(previouseLevel);

            currentLevel++;
            GameController.instance.SaveCurrentLevel(currentLevel);
            GameController.instance.ResetPointCollectAtCurrentLevel();
            
            GetAllPointCountAtLevel();
            levelsDict[previouseLevel].levelComplete = true;
            levelsDict[previouseLevel].gameObject.SetActive(false);
            if (levelsDict.ContainsKey(currentLevel))
            {

                GameController.instance.GetGameAnalytic.StartLevel(currentLevel);
                GetCurrentLevelWinPercent(currentLevel);
                levelsDict[currentLevel].LevelInit();
                levelsDict[currentLevel].gameObject.SetActive(true);
                InitLevelReferenceLampBrokeParticle(currentLevel);
                Vector3 playerPos = new Vector3(levelsDict[currentLevel].playerPositionAtStart.x, levelsDict[currentLevel].playerPositionAtStart.y, 0);
                FindObjectOfType<Player>().GetComponent<Rigidbody>().velocity = Vector3.zero;
                FindObjectOfType<Player>().transform.position = playerPos;

            }
            else
            {

                //Debug.Log("Level Complete");

            }

        }
        //Debug.Log(currentLevel);
        //Debug.Log(levelsDict.Count);
        if (currentLevel - 1  == levelsDict.Count && !levelExpire) {
            currentLevel = currentLevel - 1;
            levelExpire = true;
            
        }

        if (levelExpire) {

            //if level count expire set random exist level
            Debug.Log("LevelExpire");

            previouseLevel = currentLevel;
            currentLevel = Random.Range(1, levelsDict.Count);
            //Debug.Log(previouseLevel);
            GameController.instance.SaveCurrentLevel(currentLevel);
            GetAllPointCountAtLevel();
            //Debug.Log(previouseLevel);
            levelsDict[previouseLevel].gameObject.SetActive(false);
            if (levelsDict.ContainsKey(currentLevel))
            {

                levelsDict[currentLevel].gameObject.SetActive(true);

                Vector3 playerPos = new Vector3(levelsDict[currentLevel].playerPositionAtStart.x, levelsDict[currentLevel].playerPositionAtStart.y, 0);
                FindObjectOfType<Player>().GetComponent<Rigidbody>().velocity = Vector3.zero;
                FindObjectOfType<Player>().transform.position = playerPos;

            }

        }

    }

}
