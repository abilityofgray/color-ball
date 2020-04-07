using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class Platform : MonoBehaviour
{

    private int PointPrice;

    [SerializeField]
    protected TextMeshPro pointCounterTextUI;

    public Material blinkMaterial;

    public bool isDestroy = false;
    public bool activate = false;
    public bool FinishPlatfrom;
    public bool DontSavePlayerPos;
    public bool NoPointPlatform;
    public bool LevelBorderPlatform;
    public Vector3 positionAtStart;
    public RectTransform positionAtStartText;
    public Color colorTextAtStart;

    public float customEmission;

    public AnimationCurve blinkAction;

    AudioSource _audioSource;

    public int GetPointCost { get { return PointPrice; } }
    public int SetPointCost { set { PointPrice = value; } }

    float timeWithoutCollision;

    float emission;
    float timer;
    //TODO:
    float comboCounter;

    private Lamp lamp;

    Vector3 mVect;

    Lamp lampObject;
    CapsuleCollider capsuleCol;
    MeshRenderer meshRender;
    MeshCollider meshCol;


    public MeshRenderer GetMeshRenderer { get { return meshRender; } }

    // Start is called before the first frame update
    void Awake()
    {

        //pointCounterTextUI = this.GetComponentInChildren<TextMeshPro>();
        positionAtStart = this.transform.position;
        //positionAtStartText = pointCounterTextUI.rectTransform;
        //colorTextAtStart = pointCounterTextUI.color;


    }

    private void Start()
    {

        //pointCounterTextUI = transform.parent.GetComponent<Level>().pointTextUI.GetComponent<TextMeshPro>();




    }

    void EmissionBlinkTesting()
    {

        timer = Time.deltaTime;
        //_emission = Mathf.PingPong(Time.time, 0.85f);
        emission = blinkAction.Evaluate(Time.time);
        Debug.Log(emission);
        //
        Material baseMaterial = GetComponent<Renderer>().material;
        Color baseColor = baseMaterial.GetColor("_Color");
        float abjustedIntensity = emission - 0.4169F;
        //baseColor.
        //baseColor *= Mathf.PingPong(Time.time, abjustedIntensity);
        baseColor *= Mathf.Pow(3.5F, abjustedIntensity);
        baseMaterial.SetColor("_EmissionColor", baseColor);


    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.GetComponent<Player>())
        {


            //Exception popup beccause partycle is only one and she already remove from current gameobject
            //maybe if partycle replaced instntinate new
            //Check for if children exist


            if (!FinishPlatfrom)
            {

                Player _player = collision.gameObject.GetComponent<Player>();
                ParticleSystem _particle = GetComponentInChildren<ParticleSystem>();
                if (_particle != null) _particle.transform.parent = _player.transform;

            }





        }

    }

    void ReflectPlayer(Player player, ContactPoint contactPoint)
    {

        Rigidbody _playerRB = player.GetComponent<Rigidbody>();

        Vector3 reflectVec = Vector3.Reflect(_playerRB.velocity, contactPoint.normal).normalized;

        _playerRB.velocity = reflectVec * 35.25f;

    }

    public void PlayerFinishReflect() {

        


    }

    void PlayPlayerContactParticle(Player player,PlayerParticle playerParticle, ContactPoint contactPoint)
    {

        //Debug.Log("playPlayercontactparticle");

        //PlayerParticle playerParticle = FindObjectOfType<PlayerParticle>();

        if (playerParticle != null)
        {

            // _particle.transform.parent = null;
            //_particle.transform.parent = transform;
            playerParticle.transform.position = contactPoint.point;
            playerParticle.PlayParticle();
            player.GetAnimation.Play();

        }

    }

    void PlayAudioContact()
    {

        
        if (_audioSource != null)
        {

            AudioController.instance.PlayBrokeLamp(_audioSource);

        }

    }

    void DestroyLampElement()
    {


        PlayAudioContact();
        lamp.LampBrokeParticle.PlayBroke(this.transform.position);


        if (lampObject)
        {

            lampObject.Blink();

        }

        if (capsuleCol)
        {

            capsuleCol.enabled = false;
            meshRender.enabled = false;
        }
        else
        {

            meshRender.enabled = false;
            meshCol.enabled = false;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player)
        {

            if (lamp) lamp.TouchLampElementEvent();

            ContactPoint contactPoint = collision.contacts[0];

            PlayPlayerContactParticle(player, player.playerContactParticle, contactPoint);

            if (!FinishPlatfrom)
            {

                ReflectPlayer(player, contactPoint);

            }

            if (isDestroy)
            {

                DestroyLampElement();

                //LampBrokenParticle lampBroke = FindObjectOfType<LampBrokenParticle>();
                //lampBroke.transform.position = contactPoint.point;
                //lampBroke.PlayBroke();
                

                GameController.instance.PlatformSeriesTouch(GetPointCost);

            }

            if (LevelBorderPlatform)
            {

                GameController.instance.GamePlayerReachLevelBorder();

            }

            if (FinishPlatfrom) FinishPlatform(player);


        }

    }

    void FinishPlatform(Player player) {

        player.FreezePlayer();
        GetComponentInParent<Lamp>().CallLampBlinkInChildren();
        //GetComponentInParent<Lamp>().StartEndlessBlinkAllLamp();

    }

    public void RestartPlatform()
    {

        

        activate = false;

        GetComponent<Renderer>().enabled = true;
        
        if (transform.parent.TryGetComponent<Lamp> (out Lamp lamp)) {

            
            lamp.isTouched = false;
            lamp.StopAllBlink();

        }
        

        if (TryGetComponent<CapsuleCollider>(out CapsuleCollider capCol ))
        {

            capCol.enabled = true;

        }

        if (TryGetComponent<MeshCollider> (out MeshCollider meshCol))
        {

            meshCol.enabled = true;

        }



    }

    public void TextRollBack()
    {



    }

    public void InitPlatform()
    {

        
        RestartPlatform();

        if (transform.parent.TryGetComponent<Lamp>(out var tempLamp)) {

            lamp = tempLamp;
            lampObject = tempLamp;

        }
        if (TryGetComponent<AudioSource>(out var tempAudio)) _audioSource = tempAudio;
        //if (transform.parent.TryGetComponent<Lamp>(out var lampObjectTemp)) lampObject = lampObjectTemp;
        if (TryGetComponent<CapsuleCollider>(out var capsuleColTemp)) capsuleCol = capsuleColTemp;
        if (TryGetComponent<MeshRenderer>(out var meshRendererTemp)) meshRender = meshRendererTemp;
        if (TryGetComponent<MeshCollider>(out var meshColTemp)) meshCol = meshColTemp;
        
    }

    /*
    private void OnDisable()
    {

        //pointCounterTextUI = this.GetComponentInChildren<TextMeshPro>();
        //positionAtStart = this.transform.position;


    }
    */

    

    //old blink
    IEnumerator BlinkPlatform()
    {

        float timer = 0;

        Material defaultMaterial = GetComponent<MeshRenderer>().material;
        GetComponent<MeshRenderer>().material = blinkMaterial;

        while (true)
        {

            timer += Time.deltaTime;
            if (timer > 0.1f)
            {

                GetComponent<MeshRenderer>().material = defaultMaterial;
                timer = 0;

            }

            yield return null;

        }

    }

    float blinkTimer = 0;

    IEnumerator BlinkLampStyle()
    {

        blinkTimer = 0;

        while (blinkTimer < 1.5f)
        {

            emission = blinkAction.Evaluate(Time.time);
            blinkTimer += Time.deltaTime;
            Material baseMaterial = GetComponent<Renderer>().material;
            Color baseColor = baseMaterial.GetColor("_Color");
            float abjustedIntensity = emission - 0.4169F;

            baseColor *= Mathf.Pow(3.5F, abjustedIntensity);
            baseMaterial.SetColor("_EmissionColor", baseColor);

            yield return new WaitForSeconds(.1f);

        }

    }

    IEnumerator BlinkSound()
    {

        while (true)
        {
            Debug.Log("audioSource");
            //_audioSource.Play();
            //_audioSource.pitch = blinkAction.Evaluate(Time.time);

            yield return null;


        }

    }

}
