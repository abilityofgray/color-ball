using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameAnalytics : MonoBehaviour
{

    public static GameAnalytics instance = null;
    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
            instance = this;

    }

    public void StartGame() {

        AnalyticsEvent.GameStart();

    }

    public void EndGame() {

        AnalyticsEvent.GameOver();

    }

    public void StartLevel(int levelIndex) {

        AnalyticsEvent.LevelStart(levelIndex.ToString());

    }

    /// <summary>
    /// Player restart level in some reason (die or not collect request point)
    /// </summary>
    /// <param name="levelIndex"></param>
    public void LevelRestart(int levelIndex) {

        AnalyticsEvent.LevelFail(levelIndex.ToString());

    }

    public void LevelComplete(int levelIndex) {

        AnalyticsEvent.LevelComplete(levelIndex.ToString());

    }
    
}
