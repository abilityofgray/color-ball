using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{

    Button menuButton;

    // Start is called before the first frame update
    void Start()
    {

        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(ButtonMenu);

    }

    void ButtonMenu() {

        GameController.instance.GamePause();

    }
}
