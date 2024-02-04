
using UnityEngine;
//事件相關須套用
using UnityEngine.Events;

//音樂相關的事件訂閱
//資產文件
[CreateAssetMenu(menuName = "Event/PlayAudioEventSO")]

public class PlayAudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;

    
    public void RaiseEvent(AudioClip audioClip)
    {
        OnEventRaised?.Invoke(audioClip);
    }
}
