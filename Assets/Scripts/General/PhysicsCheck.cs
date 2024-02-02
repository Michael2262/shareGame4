using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//一定要有的組件，沒有的會自動添加
//[RequireComponent(typeof(CapsuleCollider2D),typeof(BoxCollider2D))]

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    private PlayerController playerController;
    private Rigidbody2D rb;

    [Header("檢測參數")]
    public bool manual;
    //有些東西只有在玩家角色身上才要啟用，增加手動開關
    public bool isPlayer;
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
    public bool onWall;


    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        

        if (!manual) 
        {
            //包圍框（Bounds）
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            //直接使用上一行的值
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
            
        }
        //只有在玩家角色身上才要啟用，以手動開關
        if (isPlayer) 
            playerController = GetComponent<PlayerController>();

    }

    private void Update()
    {
        check();

    }

    void check() 
    {
        if (!onWall)
            faceDir = new Vector3(-transform.localScale.x, bottomOffset.y, 0);
        else
            faceDir = new Vector3(-transform.localScale.x, 0, 0);
        //檢測地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + (bottomOffset* -faceDir.x), checkRaduis, groundLayer);
        
        //牆體判斷
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);

        //蹬牆中判斷
        if(isPlayer)
            onWall = (touchLeftWall &&playerController.inputDirection.x<0f  || touchRightWall && playerController.inputDirection.x>0f) && rb.velocity.y < 0f;
    }

    //Gizmo輔助線
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + (bottomOffset * -faceDir.x), checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
