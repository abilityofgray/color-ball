using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGainVisualisationPool : MonoBehaviour
{
    [SerializeField]
    private PointGainVisualisationText textPrefab;
    [SerializeField]
    private MainUI mainUI;
    // Start is called before the first frame update
    
    Queue<PointGainVisualisationText> textVisualPool = new Queue<PointGainVisualisationText>();


    public static PointGainVisualisationPool instance = null;
    void Start()
    {

        if (instance == null) instance = this;
        InitPool(5);

    }

    void InitPool(int count) {

        for (int i = 0; i < count; i++) {

            AddObject();

        }


    }

    public void AddObject() {

        PointGainVisualisationText textObject = Instantiate(textPrefab);
        textObject.transform.parent = mainUI.transform;
        textObject.transform.localPosition = textPrefab.transform.localPosition;
        textObject.InitObject();
        textObject.gameObject.SetActive(false);
        textVisualPool.Enqueue(textObject);

    }

    public void GetObject(string textValue) {

        //Debug.Log("Get text visualisation");
        if (textVisualPool.Count == 0) AddObject();

        PointGainVisualisationText textObject = textVisualPool.Dequeue();

        textObject.SetText(textValue);
        textObject.gameObject.SetActive(true);
        textObject.StartAnimation();

    }

    public void BackToPool(PointGainVisualisationText textObject) {

        textObject.ResetY();
        textObject.gameObject.SetActive(false);
        textVisualPool.Enqueue(textObject);

    }
}
