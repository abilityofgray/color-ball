using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    Player _player;
    Rigidbody _playerRBody;
    ParticleSystem _particle;
    //default
    public float movingSpeed = 2.25f;

    float screenCenterX;
    float screenCenterY;
    float mouseDownTimeCount;
    public AnimationCurve powerAcceleration;
    // Start is called before the first frame update
    void Start()
    {
        screenCenterX = Screen.width * 0.5f;
        screenCenterY = Screen.height * 0.5f;
        _player = FindObjectOfType<Player>();
        _playerRBody = _player.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        //TouchDetector();
        //MouseScreenPositionDetector();
        MouseOneTouchController();

    }

    void MouseOneTouchController() {

        
        //Maybe for this have strange behaivoure ball in game
        if (Input.GetMouseButtonDown(0) && GameController.instance.gameState == GameController.GameState.InGame)
        {

            
            _playerRBody.velocity = (Vector3.right + Vector3.up) * 20f;
            //_player.GetComponent<AudioSource>().clip = AudioController.instance.WhooSoundPlayerMoveTouch();
            //_player.GetComponent<AudioSource>().Play();

        }
        

        if (Input.GetMouseButton(0) && GameController.instance.gameState == GameController.GameState.InGame) {

            mouseDownTimeCount += Time.deltaTime * 3.5f;
            //Debug.Log(_playerRBody.velocity);
            if (mouseDownTimeCount <= 2.5f && _playerRBody.velocity.y < 20f) {

                //By default
                //_playerRBody.velocity = (new Vector3(1.25f , 0, 0) + new Vector3(0, 1 * mouseDownTimeCount, 0)) * 15.25f;
                _playerRBody.velocity = (new Vector3(1.25f, 0, 0) + new Vector3(0, 1 * powerAcceleration.Evaluate(mouseDownTimeCount), 0)) * 10.25f;

                //TODO: later
               // _player.GetComponent<AudioSource>().clip = AudioController.instance.WhooSoundPlayerMoveTouch();
                //_player.GetComponent<AudioSource>().Play();


            }
            
            //Debug.Log(mouseDownTimeCount);

        }
        if (Input.GetMouseButtonUp(0)) {

            mouseDownTimeCount = 0;

        }
    }

    void MouseScreenPositionDetector() {

        if (Input.GetMouseButtonDown(0)) {

            Vector3 mousePos = Input.mousePosition;

            if (mousePos.x > screenCenterX)
            {

                //Debug.Log("Right");
                _playerRBody.velocity = (Vector3.right + Vector3.up) * 10f;

            }
            else if (mousePos.x < screenCenterX) {

                //Debug.Log("Left");
                //_playerRBody.velocity = Vector3.left * movingSpeed;
            }

            if (mousePos.y > screenCenterY) {

                //Debug.Log("UP");
                //_playerRBody.velocity = Vector3.up * movingSpeed;

            }
            else if(mousePos.y < screenCenterY)
            {

                //_playerRBody.velocity = Vector3.down * movingSpeed;
                //Debug.Log("Down");

            }


        }


    }

    void TouchDetector() {

        if (Input.touchCount > 0) {
            
            Touch firstTouch = Input.GetTouch(0);

            if (firstTouch.phase == TouchPhase.Began) {

                if (firstTouch.position.x > screenCenterX)
                {

                    //right
                    //make yore code here;
                    Debug.Log("Right");

                }
                else if (firstTouch.position.x < screenCenterX)
                {

                    //left
                    //make yore code here
                    Debug.Log("Left");

                }

                if (firstTouch.position.y > screenCenterY) {

                    //up 
                    Debug.Log("up");

                }

                if (firstTouch.position.y < screenCenterY) {

                    Debug.Log("down");
                    //down

                }

            }


        }

    }

}



