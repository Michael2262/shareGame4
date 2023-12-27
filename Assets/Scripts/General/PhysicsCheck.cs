using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;

    [Header("�˴��Ѽ�")]
    public bool manual;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRaduis;

    public LayerMask groundLayer;    
    
    [Header("���A")]
    public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;


    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();

        if (!manual) 
        {
            //�]��ء]Bounds�^
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            //�����ϥΤW�@�檺��
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
            
        }

    }

    private void Update()
    {
        check();
    }

    void check() 
    {
        //�˴��a��
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);

        //����P�_
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);

    }

    //Gizmo���U�u
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
