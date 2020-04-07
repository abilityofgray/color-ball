using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointGainVisualisationText : MonoBehaviour
{

    TextMeshProUGUI textMeshPro;
    Animation animationComp;
    RectTransform rectTransform;
    float defaultPosY;

    public void SetText(string value) {

        textMeshPro.text = "+" + value;

    }

    public void InitObject() {

        textMeshPro = GetComponent<TextMeshProUGUI>();
        animationComp = GetComponent<Animation>();
        rectTransform = GetComponent<RectTransform>();
        defaultPosY = rectTransform.transform.localPosition.y;

    }

    public void StartAnimation() {

        animationComp.Play();
        StartCoroutine(FadeText());
        StartCoroutine(PopUpTextAnimation(1f));

    }

    public void ResetY() {

        rectTransform.transform.localPosition = new Vector3(0, defaultPosY, 0);

    }
    public IEnumerator FadeText()
    {

        for (float f = 1f; f >= 0; f -= 0.2f)
        {

            Color c = textMeshPro.color;
            c.a = f;
            textMeshPro.color = c;
            yield return new WaitForSeconds(.1f);
        }

        PointGainVisualisationPool.instance.BackToPool(this);

    }
    public IEnumerator PopUpTextAnimation(float speed)
    {

        //Debug.Log(rectTransform.position.y);
        while (rectTransform.localPosition.y <= 500f)
        {
            //x Random.Range(-34, 34)
            
            rectTransform.Translate(new Vector3( 0, +speed, 0));

            yield return null;

        }

        //PointGainVisualisationPool.instance.BackToPool(this);
        //Destroy(this.gameObject);
    }

    
}
