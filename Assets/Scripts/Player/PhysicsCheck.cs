using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("浪代把计")]
    public Vector2 bottomOffset;
    public float checkRaduis;

    public LayerMask groundLayer;    
    
    [Header("篈")]
    public bool isGround;
    
    private void Update()
    {
        check();
    }

    void check() 
    {
        //浪代
        isGround = Physics2D.OverlapCircle((Vector2)transform.position, checkRaduis, groundLayer);
    }

    //Gizmo徊絬
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}
