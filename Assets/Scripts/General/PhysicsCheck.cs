using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//一定要有的組件，沒有的會自動添加
[RequireComponent(typeof(CapsuleCollider2D),typeof(BoxCollider2D))]

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    

    [Header("檢測參數")]
    public bool manual;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRaduis;
    public Vector3 faceDir;

    public LayerMask groundLayer;    
    
    [Header("狀態")]
    public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;


    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        

        if (!manual) 
        {
            //包圍框（Bounds）
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            //直接使用上一行的值
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
        //檢測地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + (bottomOffset* -faceDir.x), checkRaduis, groundLayer);

        //牆體判斷
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);

    }

    //Gizmo輔助線
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + (bottomOffset * -faceDir.x), checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
