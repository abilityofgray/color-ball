using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    //TODO: refactor to AudioSource
    public static AudioController instance = null;
    // Start is called before the first frame update

    [SerializeField] private AudioData audioDataSource;

    AudioSource audioSource;


    void Start()
    {

        if (instance == null) instance = this;
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayerCollideAudio(AudioSource targetAudioSource) {

        targetAudioSource.clip = audioDataSource.CollideAudio;
        if (!targetAudioSource.isPlaying) targetAudioSource.Play();

    }

    public void UprisePlayerPoint(AudioSource targetAudioSource) {

        //Debug.Log("Point UpRaise");
        targetAudioSource.volume = 0.15f;
        targetAudioSource.clip = audioDataSource.PointUpRiseAudio;
        if (!targetAudioSource.isPlaying) targetAudioSource.Play();

    }
    public void PlayerCountNewRecord(AudioSource targetAudioSource) {



    }


    public void ComboPlayerPoint(AudioSource targetAudioSource) {

        targetAudioSource.volume = 0.5f;
        targetAudioSource.clip = audioDataSource.ComboPointAudio;
        //if (!targetAudioSource.isPlaying) targetAudioSource.Play();

    }

    public void ComboPlayerInterrupt() {



    }

    public void PlayBrokeLamp(AudioSource targetAudioSource) {

        targetAudioSource.volume = 0.15f;
        targetAudioSource.pitch = 1.5f;
        targetAudioSource.clip = audioDataSource.BrokenLamp;
        if (!targetAudioSource.isPlaying) {

            targetAudioSource.Play();

        }
        

    }

    public void MainUIButtonClick(AudioSource targetAudioSource) {

        targetAudioSource.clip = audioDataSource.ComboPointAudio;
        targetAudioSource.Play();

    }

    public void UserInGameInputSound(AudioSource targetAudioSource) {

        targetAudioSource.clip = audioDataSource.ComboPointAudio;
        targetAudioSource.Play();

    }

    public void StartComboSeries(AudioSource targetAudioSource) {

        targetAudioSource.volume = 0.25f;
        targetAudioSource.clip = audioDataSource.StartComboSeries;
        //if (targetAudioSource.isPlaying) targetAudioSource.Play();


    }

    public AudioClip WhooSoundPlayerMoveTouch() {

        return audioDataSource.WhooSoundTouchAccelHold;

    }

    
}
