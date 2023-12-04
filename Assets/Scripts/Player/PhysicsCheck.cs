using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("檢測參數")]
    public Vector2 bottomOffset;
    public float checkRaduis;

    public LayerMask groundLayer;    
    
    [Header("狀態")]
    public bool isGround;
    
    private void Update()
    {
        check();
    }

    void check() 
    {
        //檢測地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position, checkRaduis, groundLayer);
    }

    //Gizmo輔助線
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}
