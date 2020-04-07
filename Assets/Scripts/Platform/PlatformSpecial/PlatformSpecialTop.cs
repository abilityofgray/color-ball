using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformSpecialTop : Platform
{

    PlatfromSpecial _parentPlatform;
    Rigidbody _rB;
    //TextMeshPro text;
    Animation _animation;
    GameObject arrowUI;


    public GameObject arrowTop;
    public GameObject arrowBottom;
    public GameObject arrowLeft;
    public GameObject arrowRight;


    public enum PlatformOrientation {

        Top,
        Left,
        Right,
        Bottom

    }

    public PlatformOrientation platformOrientation;

    private void Start()
    {

        _parentPlatform = GetComponentInParent<PlatfromSpecial>();
        _animation = GetComponentInChildren<Animation>();
        _rB = GetComponent<Rigidbody>();
        pointCounterTextUI = this.GetComponentInChildren<TextMeshPro>();
        positionAtStartText = pointCounterTextUI.rectTransform;

        arrowTop.SetActive(false);
        arrowBottom.SetActive(false);
        arrowLeft.SetActive(false);
        arrowRight.SetActive(false);

        PlatformArrowUIDirection();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<Player>()) {

            Player _player = collision.gameObject.GetComponent<Player>();
            ContactPoint point = collision.GetContact(0);
            PlayerPlatformReflected(_player, collision, 5.25f);

            if (point.normal == new Vector3(0, -1, 0)) {

                Debug.Log("Top");
                PlatformTouchDirectionBehaviour(collision);

            }

            if (point.normal == new Vector3(0, 1, 0))
            {

                Debug.Log("Botom");
                PlatformTouchDirectionBehaviour(collision);

            }

            if (point.normal == new Vector3(1, 0, 0))
            {

                PlatformTouchDirectionBehaviour(collision);
                Debug.Log("Left");

            }

            if (point.normal == new Vector3(-1, 0, 0))
            {

                PlatformTouchDirectionBehaviour(collision);
                Debug.Log("Right");

            }
            

            

        }

    }

    void PlatformArrowUIDirection() {

        switch (platformOrientation) {

            case PlatformOrientation.Top:

                arrowTop.SetActive(true);

                break;

            case PlatformOrientation.Bottom:

                arrowBottom.SetActive(true);

                break;

            case PlatformOrientation.Right:

                arrowRight.SetActive(true);

                break;

            case PlatformOrientation.Left:

                arrowLeft.SetActive(true);

                break;

        }


    }

    void PlatformTouchDirectionBehaviour(Collision collision) {

        switch (platformOrientation)
        {

            case PlatformOrientation.Top:



                RegistredTouch(collision);
                

                break;

            case PlatformOrientation.Left:

                //collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * 15f, ForceMode.Impulse);
                RegistredTouch(collision);
                

                break;

            case PlatformOrientation.Right:

                //collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 15f, ForceMode.Impulse);
                RegistredTouch(collision);


                break;

            case PlatformOrientation.Bottom:

                //collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 15f, ForceMode.Impulse);
                RegistredTouch(collision);

                break;



        }

    }

    public void PlayerPlatformReflected(Player player, Collision collision, float reflectPower) {

        ContactPoint contact = collision.contacts[0];

        Vector3 reflectVec = Vector3.Reflect(player.GetComponent<Rigidbody>().velocity, contact.normal).normalized * reflectPower;

        collision.gameObject.GetComponent<Rigidbody>().velocity = reflectVec * reflectPower;

    }

    void RegistredTouch(Collision collision) {

        

        //pointCounterTextUI.SetText("+" + PointPrice.ToString());

        //MainUI.instance.UpdatePointCounter(PointPrice);
        //StartCoroutine(PopUpTextPointGain());
        //StartCoroutine(FadeText());
        activate = true;

        RespawnSystem.instance.SetCurrentRespawnPoint(collision.gameObject.GetComponent<Player>().transform.position);
        //GetComponent<MeshRenderer>().enabled = false;
        GetComponentInChildren<BoxCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        _rB.isKinematic = false;


    }

    /*
    public void RestartPosition() {

        //transform.position = this.positionAtStart;

        //GetComponentInChildren<BoxCollider>().enabled = true;
        //GetComponent<BoxCollider>().enabled = true;

    }
    */

}
