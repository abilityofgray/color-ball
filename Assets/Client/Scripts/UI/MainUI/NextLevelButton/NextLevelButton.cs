using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    Button nextLevelButton;
    // Start is called before the first frame update
    void Start()
    {

        nextLevelButton = GetComponent<Button>();
        nextLevelButton.onClick.AddListener(SwitchNextLevel);

    }

    void SwitchNextLevel() {

        GameController.instance.NextLevel();

    }
    

}
