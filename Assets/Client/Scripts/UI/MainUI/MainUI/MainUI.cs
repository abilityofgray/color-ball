using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{

    public static MainUI instance = null;


    [Header("Buttons")]
    public Button startButton;
    public Button nextLevelButton;
    public Button menuButton;
    
    [Header("Text components")]
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI pointCounter;
    public TextMeshProUGUI pointRecordText;
    public GameObject popUpPointText;
    public LevelCompleteText levelCompleteText;

    [Header("Game Object Components")]

    [Header("Main UI Components")]
    public PointGainVisualisation pointGainVisual;
    public Slider sliderLevelProgress;

    int count;

    AudioSource audioSource;

    [Header("Public Service variable")]
    public int currentLevelPoint;
    public int comboCount;
    public int userTapUIText;

    Animation pointCounterUIAnim;

    [Header("PopUpScreens")]
    
    public PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {

        if (instance == null) instance = this;

        pauseMenu.gameObject.SetActive(false);
        comboCount = 0;
        audioSource = GetComponent<AudioSource>();
        comboText.gameObject.SetActive(false);

        
        SwitchNextLevelButton(false);

        pointCounterUIAnim = pointCounter.GetComponent<Animation>();
        LevelCompleteText(false);

        sliderLevelProgress.value = 0;
        sliderLevelProgress.fillRect.gameObject.SetActive(false);
        //sliderLevelProgress.va 


    }

    

    public void UpdatePointCounter(int pointValue, float pointCollectAtCurentLevel)
    {
        //Remove audio play to GameController
        //audioSource.clip = AudioController.instance.UprisePlayerPoint();
        //audioSource.Play();
        pointCounterUIAnim.Play();
        
        //count += pointValue;

        pointCounter.text = pointValue.ToString();


        UpdateProgressBar(pointCollectAtCurentLevel);

        

    }


    public void UpdateProgressBar(float currentPoint) {

        if (!sliderLevelProgress.fillRect.gameObject.activeSelf) {

            sliderLevelProgress.fillRect.gameObject.SetActive(true);

        }
        float currentLevelPoint = currentPoint;
        //Debug.Log(currentPoint);
        float levelPointProgress = GameController.instance.LevelPointProgressCalculate(currentLevelPoint);
        
        
        sliderLevelProgress.value = Mathf.Clamp01(levelPointProgress / 100);
        
    }

    public void TextGainPointVisual(int pointValue) {

        //pointGainVisual.TextEnabled(pointValue.ToString());
        PointGainVisualisationPool.instance.GetObject(pointValue.ToString());

    }

    public void LevelCompleteText(bool active) {

        levelCompleteText.gameObject.SetActive(active);
        if (active) {

            levelCompleteText.GetAnimation.Play();

        }

    }
    

    public void DecreasePointCounter(int pointValue) {

        count -= pointValue;

        pointCounter.text = count.ToString();

    }

    public void ResetPointCounter() {

        count = 0;

        pointCounter.text = count.ToString();

        ResetProggresBar();

    }

    public void ResetProggresBar() {

        sliderLevelProgress.value = 0;
        sliderLevelProgress.fillRect.gameObject.SetActive(false);

    }

    public void StartScreenDisabled() {

        startButton.gameObject.SetActive(false);
        //menuButton.gameObject.SetActive(false);

    }

    public void StartScreenEnabled()
    {

        startButton.gameObject.SetActive(true);
        SwitchNextLevelButton(false);
        //menuButton.gameObject.SetActive(true);

    }

    public void SwitchNextLevelButton(bool active) {

        nextLevelButton.gameObject.SetActive(active);

    }

    public void RecordTextSetActive(bool active, float value) {

        pointRecordText.text = "record: " + value.ToString();
        pointRecordText.gameObject.SetActive(active);

    }

    

    public void SetPauseMenu(bool activate) {

        pauseMenu.gameObject.SetActive(activate);
        //
        pauseMenu.SetNextLevelButton(false);

    }

    public void SetLevelNotPass(bool activate) {


        if (activate)
        {
            pauseMenu.gameObject.SetActive(activate);
            pauseMenu.SetNextLevelButton(!activate);
        }
        else {

            pauseMenu.gameObject.SetActive(!activate);
            pauseMenu.SetNextLevelButton(activate);

        }
        

    }

    /*
    public void PlayerComboActivate() {

        
        comboCount += 1;
        
        if (comboCount > 1) {

            int tempComboCount = comboCount * 2;
            comboCount = tempComboCount;
            
            comboText.gameObject.SetActive(true);
            //comboText.GetComponent<Animation>().Play();
            comboText.text = "combo " + comboCount.ToString();
            //comboText = activate;

        }

    }
    */

    /*
    public void PlayerComdoDeactivate() {

        
        if (comboCount > 0) {

           UpdatePointCounter(comboCount);
           comboCount = 0;
           comboText.gameObject.SetActive(false);

        }
        

    }
    */

}
