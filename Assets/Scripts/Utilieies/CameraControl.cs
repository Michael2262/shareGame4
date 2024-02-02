using System.Collections;
using System.Collections.Generic;
//要using Cinemachine;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;

    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    //TODO ：場景切換後更改
    private void Start()
    {
        GetNewCameraBounds();
    }

    private void GetNewCameraBounds() 
    {
        var objs = GameObject.FindGameObjectsWithTag("Bounds");
        if (objs == null)
            return;

        GameObject obj = objs[0];

        // 获取该游戏对象上的 Collider2D 组件
        Collider2D collider = obj.GetComponent<Collider2D>();

        if (collider != null)
        {
            // 设置相机的边界形状
            confiner2D.m_BoundingShape2D = collider;

            // 清除缓存
            confiner2D.InvalidateCache();
        }
        else
        {
            Debug.LogWarning("No Collider2D component found on game object with tag 'Bounds'.");
        }
    }
}
