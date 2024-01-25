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

    //persentage�ʤ���A�U���O�������g�k
    /// <summary>
    /// ����Health���ܧ�"�ʤ���"
    /// </summary>
    /// <param name="persentage">�ʤ���GCurrent/Max</param>
    public void OnHealthChange(float persentage) 
    {
        healthImage.fillAmount = persentage;
    }
}
