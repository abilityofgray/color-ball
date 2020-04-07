using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;


    [Header ("Testing levels setting")]
    public bool ForsingLevelStart;
    public int StartAtLevel = 1;  // value by default
    public int pointCollectAtCurrentLevel;
    public float currentLevelPoint;
    public float winLimitPoint;


    [Header("Game Settings")]
    //by default 65%
    public float pointWinCollectPercent = 65;


    [SerializeField]
    private UserPointData userPointData;
    [SerializeField]
    private MainUI mainUI;
    [SerializeField]
    private LevelsController levelsController;



    int comboCounter = 0;
    bool comboActive = false;
    Player player;
    [SerializeField] private bool levelPass;

    public bool GetLevelPass { get { return levelPass; } }
    public Player GetPlayer { get { return player; } }

    public enum GameState {

        MainMenu,
        Pause,
        InGame,
        ReachWinPoint,
        GameReady,

    }

    public GameState gameState;


    void Start()
    {

        if (instance == null) instance = this;
        
        gameState = GameState.MainMenu;

        Application.targetFrameRate = 30;

        mainUI.RecordTextSetActive(false, userPointData.RecordPoint);
        if (userPointData.LevelStart == 0 && !ForsingLevelStart)
        {

            levelsController.StartAtLevel = StartAtLevel;
            

        }
        else if (!ForsingLevelStart)
        {

            levelsController.StartAtLevel = userPointData.LevelStart;

        }

        if (ForsingLevelStart) {
            
            levelsController.StartAtLevel = StartAtLevel;

        }
        
        mainUI.ResetPointCounter();
        if (userPointData.RecordPoint != 0)
        {

            mainUI.RecordTextSetActive(true, userPointData.RecordPoint);

        }

        player = FindObjectOfType<Player>();
        
        

    }

    

    public void GamePause() {

        if (gameState == GameState.Pause)
        {

            gameState = GameState.InGame;
            Time.timeScale = 1;
            mainUI.SetPauseMenu(false);


        }
        else {

            gameState = GameState.Pause;
            Time.timeScale = 0;
            mainUI.SetPauseMenu(true);

        }
        

    }

    public void ResetPointCollectAtCurrentLevel() {

        pointCollectAtCurrentLevel = 0;
        

    }

    public void ResetPointCollectAtLevelInUserData() {

        userPointData.CurrentPoint = 0;

    }

    public void GameStart() {


        gameState = GameState.GameReady;
        mainUI.StartScreenDisabled();

        currentLevelPoint = 0; //reset current level point counter

        mainUI.RecordTextSetActive(false, userPointData.RecordPoint);
        mainUI.LevelCompleteText(false);
        StartCoroutine(CounterToGivePlayerControlle());

    }

    public void GamePlayerReachFinish() {


        gameState = GameState.ReachWinPoint;
        
        if (CheckLevelCompleteCondition())
        {

            levelPass = true;
            mainUI.SwitchNextLevelButton(true);
            LevelPointAddition();
            UpdatePointRecord();
            mainUI.LevelCompleteText(true);

        }
        else {

            levelPass = false;
            mainUI.SetLevelNotPass(true);
            


        }
        
        
        

    }

    public void SaveCurrentLevel(int currentLevel) {

        userPointData.LevelStart = currentLevel;

    }

    public void GamePlayerReachLevelBorder() {

        userPointData.CurrentPoint = 0;

        currentLevelPoint = 0;

        GamePause();
       
    }



    public void UpdatePointCounter(int pointValue) {

        //AudioController.instance.UprisePlayerPoint();
        
        userPointData.CurrentPoint += pointValue;
        currentLevelPoint += pointValue;
        pointCollectAtCurrentLevel += pointValue;
        mainUI.UpdatePointCounter(userPointData.CurrentPoint, pointCollectAtCurrentLevel);

    }

    public void PointGainUIVisual(int pointValue) {

        //Debug.Log(pointValue);
        mainUI.TextGainPointVisual(pointValue);

    }

    public void UpdatePointRecord() {

        if (userPointData.CurrentPoint > userPointData.RecordPoint)
        {

            userPointData.RecordPoint = userPointData.CurrentPoint;

        }
        
        

    }

    public void LevelPointAddition()
    {



    }

    //TODO: refactor to separate class?
    public void PlatformSeriesTouch(int pointValue) {

        int pointValueCal = pointValue;
        string textMessage = "+" + pointValue.ToString();
        
        comboCounter += 1;
        
        if (comboCounter > 2 && comboCounter < 4)
        {
            
            InitComboPoint(2, pointValue, pointValueCal, textMessage);

        }
        if (comboCounter >= 4 && comboCounter < 5)
        {

            InitComboPoint(4, pointValue, pointValueCal, textMessage);

        }
        if (comboCounter >= 5)
        {

            InitComboPoint(5, pointValue, pointValueCal, textMessage);

        }
        if (comboCounter <= 2)
        {

            UpdatePointCounter(pointValueCal);
            PointGainUIVisual(pointValueCal);
            CreatePopUpPointText(textMessage);

        }

    }

    public void InitComboPoint(int comboValue, int pointValue, int pointValueCal, string textMessage) {

        pointValueCal = pointValue * comboValue;
        textMessage = "+" + pointValueCal.ToString();
        
        UpdatePointCounter(pointValueCal);
        PointGainUIVisual(pointValueCal);
        CreatePopUpComboPointText(textMessage);

    }

    public void CreatePopUpPointText(string textString) {

        Transform playerPos = player.transform;
        
        GainPointTextPool.intsance.GetObject(playerPos.position, textString);
        
    }

    public void CreatePopUpComboPointText(string textString) {

        Transform playerPos = player.transform;
        
        GainPointTextPool.intsance.GetObject(playerPos.position, textString);
        
    }

    public void PlatformSeriesTouchDisabled() {

        AudioController.instance.ComboPlayerInterrupt();
        comboCounter = 0;
        comboActive = false;
        
    }

    //TODO: refactor
    public void GameRestart() {

        
        
        ResetPointCollectAtCurrentLevel();
        FindObjectOfType<Player>().GetComponent<Rigidbody>().velocity = Vector3.zero;
        FindObjectOfType<Player>().RestartPosition();
        mainUI.ResetPointCounter();
        
        Platform[] go = FindObjectsOfType<Platform>();

        foreach (Platform pl in go) {

            pl.RestartPlatform();

        }

        Lamp[] lamps = FindObjectsOfType<Lamp>();
        foreach (Lamp lm in lamps) {

            //TODO somthing
            //Get finish Lamps and in his Lamps reset color for oprions text

        }
        Time.timeScale = 1;
        mainUI.SetPauseMenu(false);
        mainUI.ResetProggresBar();
        mainUI.LevelCompleteText(false);
        GameStart();

        player.SetGravity(true);

    }



    public void StartLevel() {



    }


    public float LevelPointProgressCalculate(float currentPoint) {

        float allLevelPoint = GetCurrentLevelAllPoint();
        winLimitPoint = (allLevelPoint / 100) * levelsController.GetWinPercentCurrentLevel;
        //25 % от всех очков уровня (1 / alllevelPoint) * 25
        //float levelPointProgress = (currentPoint * 100) / allLevelPoint;

        float levelPointProgress = (currentPoint * 100) / winLimitPoint;
        // result in percent
        return levelPointProgress;

    }

    public void NaturalPointFromPointLevel() {



    }

    public bool CheckLevelCompleteCondition() {

        bool levelPass = false;
        
        float gainedCurentPoint = userPointData.CurrentPoint; // player curent gained

        //get percent of all level
        float levelPointProgressWinCondition = LevelPointProgressCalculate(gainedCurentPoint);
        
        if (pointCollectAtCurrentLevel >= winLimitPoint) {
            
            levelPass = true;
            mainUI.ResetProggresBar();

        }
        else {

            ResetPointCollectAtLevelInUserData();

        }
        return levelPass;

    }

    public float GetCurrentLevelAllPoint() {


        return (float)levelsController.AllPointAtCurrentLevel;

    }



    public void NextLevel() {

        levelsController.NextLevelStart();
        mainUI.StartScreenEnabled();
        Time.timeScale = 1;
        mainUI.SetPauseMenu(false);
        mainUI.LevelCompleteText(false);

        player.SetGravity(true);

    }

    //Delay before give player controll
    IEnumerator CounterToGivePlayerControlle() {

        yield return new WaitForSeconds(0.25f);
        gameState = GameState.InGame;

    }

    private void OnEnable()
    {

        userPointData.CurrentPoint = 0;

    }

    private void OnDisable()
    {

        userPointData.CurrentPoint = 0;

    }



}
