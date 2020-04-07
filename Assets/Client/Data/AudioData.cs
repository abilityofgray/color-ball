using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class AudioData : ScriptableObject
{

    [SerializeField] private AudioClip collideAudio;
    [SerializeField] private AudioClip pointUpRiseAudio;
    [SerializeField] private AudioClip pointDecreaseAudio;
    [SerializeField] private AudioClip whooSoundTouchAccelHold;
    [SerializeField] private AudioClip comboPointAudio;
    [SerializeField] private AudioClip startComboSeries;
    [SerializeField] private AudioClip reachFinishAudio;
    [SerializeField] private AudioClip stopComboSeries;
    [SerializeField] private AudioClip brokenLamp;

    public AudioClip CollideAudio { get { return collideAudio; }}
    public AudioClip PointUpRiseAudio { get { return pointUpRiseAudio; } }
    public AudioClip PointDecreaseAudio { get { return pointDecreaseAudio; } }
    public AudioClip WhooSoundTouchAccelHold { get { return whooSoundTouchAccelHold; } }
    public AudioClip ComboPointAudio { get { return comboPointAudio; } }
    public AudioClip StartComboSeries { get { return startComboSeries; } }
    public AudioClip ReachFinishAudio { get { return reachFinishAudio; } }
    public AudioClip StopAudioSeries { get { return stopComboSeries; } }
    public AudioClip BrokenLamp { get { return brokenLamp; } }

}
