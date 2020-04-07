using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainPointTextPool : MonoBehaviour
{
    [SerializeField]
    private PointTextUI _textUIPoint;
    // Start is called before the first frame update
    Queue<PointTextUI> textUIPool = new Queue<PointTextUI>();

    public static GainPointTextPool intsance = null;

    void Start()
    {

        if (intsance == null) intsance = this;
        InitPool(12);
        //Debug.Log(textUIPool.Count);

    }

    void InitPool(int count) {

        for (int i = 0; i < count; i++) {

            AddObject();

        }

    }

    void AddObject() {


        PointTextUI textPointObject = Instantiate(_textUIPoint);
        textPointObject.InitObject();
        textPointObject.gameObject.SetActive(false);
        textUIPool.Enqueue(textPointObject);

    }

    public void GetObject(Vector3 playerPosition, string textMsg) {

        //Debug.Log("GetObject");
        if (textUIPool.Count == 0) AddObject();
        PointTextUI textPointObject = textUIPool.Dequeue();
       
        
        textPointObject.SetObjectStartPosition(playerPosition);
        textPointObject.SetObjectText(textMsg);
        textPointObject.gameObject.SetActive(true);
        textPointObject.StartObjectAnimation();

    }

    public void BackToPool(PointTextUI textPoint) {

        //Debug.Log("Back To pool");
        textPoint.gameObject.SetActive(false);
        textUIPool.Enqueue(textPoint);


    }
    
}
