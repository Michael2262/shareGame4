using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStarBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;

    private void Update()
    {
        if(healthDelayImage.fillAmount > healthImage.fillAmount) 
        {
            healthDelayImage.fillAmount -= Time.deltaTime*0.5f;
        }

    }

    //persentage百分比，下面是註釋的寫法
    /// <summary>
    /// 接收Health的變更"百分比"
    /// </summary>
    /// <param name="persentage">百分比：Current/Max</param>
    public void OnHealthChange(float persentage) 
    {
        healthImage.fillAmount = persentage;
    }
}
