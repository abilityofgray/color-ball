using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointGainVisualisation : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TextEnabled(string textValue) {

        PointGainVisualisationPool.instance.GetObject(textValue);
        /*
        GameObject textTemp = Instantiate(textPrefab.gameObject, transform);

        PointGainVisualisationText textSetting = textTemp.GetComponent<PointGainVisualisationText>();

        if (!textTemp.activeSelf) {

            textTemp.SetActive(true);

        }
        textSetting.SetText(textValue);
        textSetting.StartAnimation();
        */

    }
    

    
}
