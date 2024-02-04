
using UnityEngine;
//�ƥ�������M��
using UnityEngine.Events;

//���֬������ƥ�q�\
//�겣���
[CreateAssetMenu(menuName = "Event/PlayAudioEventSO")]

public class PlayAudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;

    
    public void RaiseEvent(AudioClip audioClip)
    {
        OnEventRaised?.Invoke(audioClip);
    }
}
