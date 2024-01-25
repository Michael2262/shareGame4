using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//管理所有UI的邏輯
public class UiManager : MonoBehaviour
{
    public PlayerStarBar playerStarBar;
    
    [Header("事件監聽")]
    public CharacterEventSO healthEvent;

    //事件監聽方式，訂閱healthEvent的事件
    private void OnEnable()
    {
        //訂閱方式，要把什麼代碼加進去
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnDisable()
    {
        //相反意思，把甚麼剪掉
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persenTage = character.currentHealth / character.maxHealth;
        playerStarBar.OnHealthChange(persenTage);
    }
}
