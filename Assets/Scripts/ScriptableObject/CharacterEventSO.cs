using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//�¥N�X�����ƥ�q�\
//�겣���
[CreateAssetMenu(menuName ="Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;

    //�ַQ�ҥΡA�N��ۤvcharacter�N�X�Ƕi��
    public void RaiseEvent(Character character) 
    {
        OnEventRaised?.Invoke(character);
    }

}
