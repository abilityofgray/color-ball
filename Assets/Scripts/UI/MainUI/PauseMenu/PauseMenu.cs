using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public enum PauseMenuState {

        Disabled,
        PauseGame,
        PlayerDie,

    }

    public PauseMenuState pauseMenuState = PauseMenuState.Disabled;

    [Header("Pause Menu Component")]
    public Button restartButton;
    public Button nextLevelButton;


    private void Start()
    {

        
        //Debug.Log(pauseMenuState);

    }

    public void RestartButtonActive(bool state) {

        restartButton.gameObject.SetActive(state);

    }

    public void SetNextLevelButton(bool state) {

        nextLevelButton.gameObject.SetActive(state);

    }

}
