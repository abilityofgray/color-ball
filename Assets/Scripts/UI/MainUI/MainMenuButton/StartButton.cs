using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    Button startButton;

    // Start is called before the first frame update
    void Start()
    {

        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(StartLevel);

    }

    void StartLevel() {

        GameController.instance.GameStart();

    }
}
