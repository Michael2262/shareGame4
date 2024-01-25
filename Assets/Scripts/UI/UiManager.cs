using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�޲z�Ҧ�UI���޿�
public class UiManager : MonoBehaviour
{
    public PlayerStarBar playerStarBar;
    
    [Header("�ƥ��ť")]
    public CharacterEventSO healthEvent;

    //�ƥ��ť�覡�A�q�\healthEvent���ƥ�
    private void OnEnable()
    {
        //�q�\�覡�A�n�⤰��N�X�[�i�h
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnDisable()
    {
        //�ۤϷN��A��ƻ�ű�
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persenTage = character.currentHealth / character.maxHealth;
        playerStarBar.OnHealthChange(persenTage);
    }
}
