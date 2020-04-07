using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{

    public static RespawnSystem instance = null;

    public Vector3 playerStartPosition;

    Player _player;
    Vector3 playerRespawnPosition;

    
    // Start is called before the first frame update
    void Start()
    {

        if (instance == null) instance = this;


    }

    public void RespawnPlayer() {

        _player = FindObjectOfType<Player>();
        _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _player.transform.position = playerRespawnPosition;

        //Punishment for player if he "die"
        MainUI.instance.DecreasePointCounter(10);

    }

    public void RespawnPlayerAtStartPosition() {

        LevelsController.instance.GetCurrentLevelStartPlayerPosition();

    }

    public void SetCurrentRespawnPoint(Vector3 data) {

        playerRespawnPosition = new Vector3 (data.x, data.y + 10.5f, data.z);

    }
}
