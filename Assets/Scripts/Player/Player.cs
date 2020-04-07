using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rigidBody;

    Vector3 movePoint;

    public Vector3 startPosition;


    AudioSource audioSource;
    ParticleSystem particle;
    private  Animation animation;
    /// <summary>
    /// Swipe var
    /// </summary>
    /// 
    Touch theTouch;
    Vector2 touchStartPosition, touchEndPosition;

    public PlayerParticle playerContactParticle;

    public Animation GetAnimation { get { return animation; } }

    void Start()
    {

        /*
        rigidBody = GetComponent<Rigidbody>();
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        particle = GetComponent<ParticleSystem>();
        animation = GetComponent<Animation>();
        */

        if (TryGetComponent<Rigidbody>(out var rigidbodyTemp)) rigidBody = rigidbodyTemp;
        if (TryGetComponent<AudioSource>(out var audioSourceTemp)) audioSource = audioSourceTemp;
        if (TryGetComponent<ParticleSystem>(out var particleSystemTemp)) particle = particleSystemTemp;
        if (TryGetComponent<Animation>(out var animationTemp)) animation = animationTemp;
        startPosition = transform.position;
        playerContactParticle = FindObjectOfType<PlayerParticle>();

    }

    // Update is called once per frame
    void Update()
    {

        

        /*
        if (Input.GetMouseButtonDown(0) && !MainUI.instance.startButton.IsActive()) {

            if (GameController.instance.gameState == GameController.GameState.InGame) {

                movePoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
                movePoint.z = 0;

                
                

            }
            
        }
        */

    }


    void GamePause() {



    }

    public void FreezePlayer() {

        rigidBody.velocity = Vector3.zero;

        rigidBody.useGravity = false;

    }

    public void SetGravity(bool flag) {

        rigidBody.useGravity = flag;

    }


    //TODO: seperate to another class 
    /*
    //Swipe realization
    */
    public void Swipe() {

        if (Input.touchCount > 0)
        {

            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Began)
            {

                Debug.Log("Touch:" + Time.deltaTime);
                touchStartPosition = theTouch.position;

            }

            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {

                touchEndPosition = theTouch.position;

                float x = touchEndPosition.x - touchStartPosition.x;
                float y = touchEndPosition.y - touchStartPosition.y;

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {

                    //Tapped;

                }

                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {

                    //direction x > 0 ? right : left

                }

                else
                {

                    // direction y > 0 ? up : down;

                }
            }

        }

    }

    public void PlayerReachFinish() {

        rigidBody.velocity = Vector3.zero;

    }

    private void OnCollisionEnter(Collision collision)
    {

        PlayerCollideSound();

    }

    void PlayerCollideSound() {

        AudioController.instance.PlayerCollideAudio(audioSource);
       
    }



    public void RestartPosition() {

        //TODO reset active force;

        Vector3 restartPos = FindObjectOfType<PlatformStartLevel>().gameObject.transform.position;

        transform.position = new Vector3 (restartPos.x, restartPos.y +5.5f, restartPos.z);
        

    }



}
