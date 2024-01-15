using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�@�w�n�����ե�A�S�����|�۰ʲK�[
[RequireComponent(typeof(CapsuleCollider2D),typeof(BoxCollider2D))]

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    

    [Header("�˴��Ѽ�")]
    public bool manual;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRaduis;
    public Vector3 faceDir;

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
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        //�˴��a��
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + (bottomOffset* -faceDir.x), checkRaduis, groundLayer);

        //����P�_
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);

    }

    //Gizmo���U�u
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + (bottomOffset * -faceDir.x), checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
