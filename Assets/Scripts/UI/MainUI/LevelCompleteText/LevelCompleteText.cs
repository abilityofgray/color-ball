using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCompleteText : MonoBehaviour
{

    Animation _animation;
    TextMeshProUGUI _text;

    public Animation GetAnimation { get { return _animation; } }
    public TextMeshProUGUI GetText { get { return _text; } }
    
    void Awake()
    {

        _animation = GetComponent<Animation>();
        _text = GetComponentInChildren<TextMeshProUGUI>();

    }



    
  
}
