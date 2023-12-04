using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("�˴��Ѽ�")]
    public Vector2 bottomOffset;
    public float checkRaduis;

    public LayerMask groundLayer;    
    
    [Header("���A")]
    public bool isGround;
    
    private void Update()
    {
        check();
    }

    void check() 
    {
        //�˴��a��
        isGround = Physics2D.OverlapCircle((Vector2)transform.position, checkRaduis, groundLayer);
    }

    //Gizmo���U�u
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}
