using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("�ƥ��ť")]
    public PlayAudioEventSO FxEvent;
    public PlayAudioEventSO BGMEvent;
    [Header("�����ե�")]
    public AudioSource BGMSource;
    public AudioSource FXSource;

    private void OnEnable()
    {
        
    }

}
