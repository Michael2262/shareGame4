using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("事件監聽")]
    public PlayAudioEventSO FxEvent;
    public PlayAudioEventSO BGMEvent;
    [Header("素材組件")]
    public AudioSource BGMSource;
    public AudioSource FXSource;

    private void OnEnable()
    {
        
    }

}
