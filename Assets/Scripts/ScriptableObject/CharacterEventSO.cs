using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//純代碼式的事件訂閱
//資產文件
[CreateAssetMenu(menuName ="Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;

    //誰想啟用，就把自己character代碼傳進來
    public void RaiseEvent(Character character) 
    {
        OnEventRaised?.Invoke(character);
    }

}
