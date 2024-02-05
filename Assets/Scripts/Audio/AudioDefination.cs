using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioEventSO playAudioEvent;
    public AudioClip audioClip;
    public bool playOnEnable;

    //如果外面有勾選playOnEnable，代表出現即激活，則在OnEnable主動廣播
    //給manager做接收
    private void OnEnable()
    {
        if (playOnEnable)
            PlayAudioClip();
    }

    
    public void PlayAudioClip() 
    {
        playAudioEvent.RaiseEvent(audioClip);
    }
}
