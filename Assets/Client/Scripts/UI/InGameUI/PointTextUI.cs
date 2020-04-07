using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//Entities 
public class PointTextUI : MonoBehaviour
{
    //by default = 20
    public float heightLimit = 50f;
    public float fontSize = 15f;

    // Update is called once per frame
    
    RectTransform rectTransform;

    [SerializeField]
    private Material textMeshProMatCombo;
    [SerializeField]
    private Material textMeshProDefaultMaterial;

    //This is Component
    private AudioSource audioSource;
    private Transform transformObject;
    private MeshRenderer meshRenderer;
    private TextMeshPro textUnit;
    

    public Transform GetTransfrom { get { return transformObject; } }
    public AudioSource GetAudioSource { get { return audioSource; } }
    public MeshRenderer GetMeshRenderer { get { return meshRenderer; } }
    public TextMeshPro GetTextMeshPro { get { return textUnit; } }

    public Transform SetTransfrom { set { transformObject = value; } }
    public AudioSource SetAudioSource { set { audioSource = value; } }
    public MeshRenderer SetMeshRenderer { set { meshRenderer = value; } }
    public TextMeshPro SetTextMeshPro { set { textUnit = value; } }


    private void Start()
    {

        
        
        //rectTransform = textUnit.rectTransform;
        
    }
    

    //delete
    public void PopUpTextPointGain(GameObject go, string textToAdd, Vector3 positionToPopUp) {

        GetComponent<MeshRenderer>().material = textMeshProDefaultMaterial;
        
        //stop prev coroutine
        //StopCoroutine(FadeText());
        
        //reset alfa channel color 
        //Color c = textUnit.color;
        //c.a = 1;
        //textUnit.color = c;

        //cash camera position
        
    }

    //delete
    public void PopUpTextCombo(GameObject go , string textToAdd, Vector3 positionToPopUp) {

        GetComponent<MeshRenderer>().material = textMeshProMatCombo;
        

    }

    public void AssemblyObject() {



    }

    public void SetObjectStartPosition(Vector3 position) {

        transformObject.position = position;

    }

    public void SetObjectText(string text) {

        textUnit.fontSize = fontSize;
        textUnit.SetText(text);

    }

    public void StartObjectAnimation() {

        transformObject.LookAt(Camera.main.transform.position);
        StartCoroutine(PopUpTextAnimation(0.15f));
        StartCoroutine(FadeText());

    }

    

    IEnumerator PopUpTextAnimation(float speed)
    {

        while (GetComponent<RectTransform>().localPosition.y <= heightLimit)
        {

            GetComponent<RectTransform>().Translate(new Vector3(0, +speed, 0));
            
            yield return null;

        }
        
        
    }

    public IEnumerator FadeText()
    {

        for (float f = 1f; f >= 0; f -= 0.05f)
        {

            Color c = GetComponent<TextMeshPro>().color;
            c.a = f;
            GetComponent<TextMeshPro>().color = c;
            yield return new WaitForSeconds(.1f);

        }
        

    }

    //Call in Pool when Add Object
    public void InitObject() {

        textUnit = GetComponent<TextMeshPro>();
        audioSource = GetComponent<AudioSource>();
        transformObject = GetComponent<Transform>();

    }

    

    //Add life time to object ?
    private void OnBecameInvisible()
    {

        //Debug.Log("BecomeInvisible");
        //GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<TextMeshPro>().enabled = false;

        //TODO: refactor to get reference from InitObject;
        GainPointTextPool.intsance.BackToPool(this);

    }

    
}
