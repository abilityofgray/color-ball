using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RestartButton : MonoBehaviour
{

    Button restartButton;
    // Start is called before the first frame update
    void Start()
    {

        restartButton = GetComponent<Button>();
        restartButton.onClick.AddListener(ButtonRestart);

    }

    void ButtonRestart() {

        GameController.instance.GameRestart();

    }
}
