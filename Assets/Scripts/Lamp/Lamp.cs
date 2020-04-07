﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{

    public List<Platform> lamps = new List<Platform>();
    // Start is called before the first frame update


    float blinkTimer = 0;
    float emission = 0;

    [Header ("Lamp Platform Settings")]
    public Color lampColor;
    public Color firstlampBlinkColor;
    public Color secondLampBlinkColor;
    public AnimationCurve blinkCurve;
    public bool isDetroyed = true;
    public bool isBlinkAtStart = false;
    public bool PlatformNotGainPoint = false;
    public bool isTouched = false;
    public bool isFinish = false;

    public Lamp optionsWinLamp;
    public Lamp optionsLooseLamp;

    public int PointCost = 0;

    public LampBrokenParticle lampBrokeParticle;
    public ParticleSystem finishwinConfetee;

    MeshRenderer meshRenderer;
    Platform platformObject;

    public void LampInit()
    {

        for (int i = 0; i < transform.childCount; i++) {


            if (transform.GetChild(i).TryGetComponent<MeshRenderer>(out MeshRenderer meshRendererTemp)) {

                meshRenderer = meshRendererTemp;

            }

            if (transform.GetChild(i).TryGetComponent<Platform>(out Platform platformTemp)) {

                platformObject = platformTemp;
                platformObject.InitPlatform();

            }


            

            meshRenderer.material.SetColor("_BaseColor", lampColor);
            meshRenderer.material.SetColor("_EmissionColor", lampColor);

            lamps.Add(platformObject);
            //Debug.Log(transform.childCount);
            //transform.GetChild(i).GetComponent<Platform>().SetPointCost = PointCost;
           
            if (isDetroyed)
            {

                platformObject.isDestroy = true;

                platformObject.SetPointCost = PointCost;

                

            }
            else {

                platformObject.isDestroy = false;

            }

            if (isFinish) {

                platformObject.FinishPlatfrom = true;
                

            }
        }

        //Debug.Log(lampBrokeParticle);
        
        

    }
    

    public void Blink() {

        //StartCoroutine(BlinkLampStyle());

    }


    //if one of the lapm platform touched that mean is this platform already gain point
    public void TouchLampElementEvent () {

        

        //Debug.Log("TouchLampElementEvent");
        if (!isTouched)
        {

            if (!PlatformNotGainPoint)
            {

                GameController.instance.PlatformSeriesTouch(PointCost);

            }
            isTouched = true;


        }
        else {

            GameController.instance.PlatformSeriesTouchDisabled();

        }
        //
        if (isFinish) {

            GameController.instance.GamePlayerReachFinish();

            //_playerRB.velocity = Vector3.up * 15f; //temporary off
            //StartCoroutine(BlinkLampStyle());
            if (GameController.instance.GetLevelPass)
            {

                
                ParticleSystem finParticle = GetComponentInChildren<ParticleSystem>();
                if (finParticle != null) finParticle.Play();
                

            }

        }
        

    }

    public void RestartLamp() {

        if (isDetroyed && isTouched == true) isTouched = false; 

    }

    public void StartEndlessBlinkAllLamp(Color color) {

        StartCoroutine(EndlesBlinkAllLamp(color));

    }

    public void StopEndlessBlinkAllLamp(Color color) {

        //Debug.Log("Stop Endless Blink");
        //StopCoroutine(EndlesBlinkAllLamp(color));

    }

    public void ResetToDefaultColor() {

        foreach (Platform pl in lamps)
        {

            if (pl.GetMeshRenderer)
            {

                Material baseMaterial = pl.GetMeshRenderer.material;

                baseMaterial.SetColor("_EmissionColor", lampColor);
                baseMaterial.SetColor("_BaseColor", lampColor);

            }

        }

    }
    
    IEnumerator EndlesBlinkAllLamp(Color color) {

        blinkTimer = 0;

        //Debug.Log("EndlessBlinkLamp");

        while (true)
        {

            emission = blinkCurve.Evaluate(Time.time);
            blinkTimer += Time.deltaTime;
            
            //yield return null;
            yield return new WaitForSeconds(.2f);

            foreach (Platform pl in lamps)
            {

                if (pl.GetMeshRenderer)
                {
                   
                    Material baseMaterial = pl.GetMeshRenderer.material;

                    Color baseColor = baseMaterial.GetColor("_BaseColor");
                    float abjustedIntensity = emission - 0.4169F;
                    baseColor *= Mathf.Pow(1.5F, abjustedIntensity);
                    baseMaterial.SetColor("_EmissionColor", color);
                    baseMaterial.SetColor("_BaseColor", color);

                }

            }

            
            yield return new WaitForSeconds(.55f);

            foreach (Platform pl in lamps)
            {

                if (pl.GetMeshRenderer)
                {

                    Material baseMaterial = pl.GetMeshRenderer.material;

                    Color baseColor2 = baseMaterial.GetColor("_BaseColor");

                    float abjustedIntensity2 = emission - 0.4169F;
                    baseColor2 *= Mathf.Pow(1.5F, abjustedIntensity2);
                    baseMaterial.SetColor("_EmissionColor", lampColor);
                    baseMaterial.SetColor("_BaseColor", lampColor);
                    
                }

            }
            

        }



    }

    public void StartSegmentsBlinkLamp() {

        StartCoroutine(SegmentsBlinkLamp());

    }

    public void StopSegmentsBlinkLamp() {

        StopCoroutine(SegmentsBlinkLamp());

    }

    IEnumerator SegmentsBlinkLamp() {

        
        while (true)
        {
            
            emission = blinkCurve.Evaluate(Time.time);
            
            foreach (Platform pl in lamps)
            {

                yield return new WaitForSeconds(.2f);
                if (pl.GetMeshRenderer)
                {


                    
                    Material baseMaterial = pl.GetMeshRenderer.material;

                    Color baseColor = baseMaterial.GetColor("_BaseColor");
                    float abjustedIntensity = emission - 0.4169F;
                    baseColor *= Mathf.Pow(1.5F, abjustedIntensity);
                    baseMaterial.SetColor("_EmissionColor", firstlampBlinkColor);
                    baseMaterial.SetColor("_BaseColor", firstlampBlinkColor);
                    

                    
                }
                
            }

            foreach (Platform pl in lamps)
            {

                
                if (pl.GetMeshRenderer)
                {

                    
                    Material baseMaterial = pl.GetMeshRenderer.material;

                    Color baseColor2 = baseMaterial.GetColor("_BaseColor");

                    float abjustedIntensity2 = emission - 0.4169F;
                    baseColor2 *= Mathf.Pow(1.5F, abjustedIntensity2);
                    baseMaterial.SetColor("_EmissionColor", lampColor);
                    baseMaterial.SetColor("_BaseColor", lampColor);
                    yield return new WaitForSeconds(.2f);

                }

            }
            

        }

    }


    public void StartSingleSegmentBlinkLamp() {

        StartCoroutine(SingleSegmentBlinkLamp());

    }

    public void StopSingleSegmentBlinkLamp()
    {

        StopCoroutine(SingleSegmentBlinkLamp());

    }

    IEnumerator SingleSegmentBlinkLamp()
    {

        
        blinkTimer = 0;

        while (blinkTimer < 2.5f)
        {
            Debug.Log("Blink");
            emission = blinkCurve.Evaluate(Time.time);
            blinkTimer += Time.deltaTime;

            foreach (Platform pl in lamps) {

                //Debug.Log(lI);
                if (pl.gameObject.GetComponent<MeshRenderer>())
                {

                    Material baseMaterial = pl.gameObject.GetComponent<MeshRenderer>().material;
                    
                    Color baseColor = baseMaterial.GetColor("_BaseColor");
                    float abjustedIntensity = emission - 0.4169F;
                    baseColor *= Mathf.Pow(1.5F, abjustedIntensity);
                    baseMaterial.SetColor("_EmissionColor", firstlampBlinkColor);
                    baseMaterial.SetColor("_BaseColor", firstlampBlinkColor);
                    yield return new WaitForSeconds(.2f);

                    
                    Color baseColor2 = baseMaterial.GetColor("_BaseColor");
                    
                    float abjustedIntensity2 = emission - 0.4169F;
                    baseColor2 *= Mathf.Pow(1.5F, abjustedIntensity);
                    baseMaterial.SetColor("_EmissionColor", lampColor);
                    baseMaterial.SetColor("_BaseColor", lampColor);
                    yield return new WaitForSeconds(.2f);

                }

            }
            


        }

    }

    public void StartColorChangeSegmentBySegment()
    {

        StartCoroutine(ColorChangeSegmentBySegment());

    }

    public void StopColorChangeSegmentBySegment()
    {

        StopCoroutine(ColorChangeSegmentBySegment());

    }

    IEnumerator ColorChangeSegmentBySegment() {

        while (true)
        {

            emission = blinkCurve.Evaluate(Time.time);
            blinkTimer += Time.deltaTime;

            foreach (Platform pl in lamps)
            {

                if (pl.GetMeshRenderer)
                {
                    yield return new WaitForSeconds(.05f);

                    Material baseMaterial = pl.GetMeshRenderer.material;

                    Color baseColor = baseMaterial.GetColor("_BaseColor");
                    float abjustedIntensity = emission - 0.4169F;
                    baseColor *= Mathf.Pow(1.5F, abjustedIntensity);
                    baseMaterial.SetColor("_EmissionColor", firstlampBlinkColor);
                    baseMaterial.SetColor("_BaseColor", firstlampBlinkColor);

                }

            }

        }

    }

    public void OptionTextLampReset() {

        foreach (Platform pl in lamps)
        {

            if (pl.GetMeshRenderer)
            {
                

                Material baseMaterial = pl.GetMeshRenderer.material;

                Color baseColor = baseMaterial.GetColor("_BaseColor");
                
                baseMaterial.SetColor("_EmissionColor", lampColor);
                baseMaterial.SetColor("_BaseColor", lampColor);

            }

        }

    }

    public void CallLampBlinkInChildren() {
        
        //Debug.Log("CallLampBlinkInChildren()");
        if (GameController.instance.CheckLevelCompleteCondition())
        {

            //win
            /*
            Vector3 cameraPos = Camera.main.transform.position;
            Vector3 playerPos = GameController.instance.GetPlayer.gameObject.GetComponent<Transform>().position;
            Vector3 winTextPos = new Vector3(playerPos.x + 4.25f, playerPos.y + 3.5f, playerPos.z +3.25f);
            optionsWinLamp.gameObject.transform.position = winTextPos;
            optionsWinLamp.gameObject.transform.LookAt(cameraPos);
            optionsWinLamp.StartColorChangeSegmentBySegment();
            */
            StartEndlessBlinkAllLamp(firstlampBlinkColor);
            finishwinConfetee.Play();


        }
        else {

            //loose
            /*
            Vector3 cameraPos = Camera.main.transform.position;
            Vector3 playerPos = GameController.instance.GetPlayer.gameObject.GetComponent<Transform>().position;
            Vector3 winTextPos = new Vector3(playerPos.x + 4.25f, playerPos.y + 3.5f, playerPos.z + 3.25f);
            optionsLooseLamp.gameObject.transform.LookAt(cameraPos);
            optionsLooseLamp.StartColorChangeSegmentBySegment();
            */
            StartEndlessBlinkAllLamp(secondLampBlinkColor);

        } 
        

    }

    public void StopAllBlink() {

        
        //StopEndlessBlinkAllLamp(firstlampBlinkColor);
        StopAllCoroutines();
        ResetToDefaultColor();

    }
    private void OnEnable()
    {

        //TODO: refactor  slow down loading an Editor and in end device
        //lampBrokeParticle = FindObjectOfType<LampBrokenParticle>();

    }
}