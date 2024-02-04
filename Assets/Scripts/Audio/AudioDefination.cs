using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioEventSO playAudioEvent;
    public AudioClip audioclip;
    public bool playOnEnable;

    //�p�G�~�����Ŀ�playOnEnable�A�N��X�{�Y�E���A�h�bOnEnable�D�ʼs��
    //��manager������
    private void OnEnable()
    {
        if (playOnEnable)
            PlayAudioClip();
    }

    
    public void PlayAudioClip() 
    {
        playAudioEvent.OnEventRaised(audioclip);
    }
}
