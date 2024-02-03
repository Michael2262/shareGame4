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
    //利用faceDir實現轉向時調整地面判斷點
    public Vector2 faceDir;

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
        //檢測地面
        //有两个向量 v1 = (x1, y1) 和 v2 = (x2, y2)，它们的逐分量相乘结果就是 (x1 * x2, y1 * y2)。这个操作在数学上也称为点乘或者内积
        if (!onWall) //正常狀況
        {
            faceDir = new Vector2(-transform.localScale.x, 0);
            isGround = Physics2D.OverlapCircle((Vector2)transform.position + (bottomOffset * -faceDir.x), checkRaduis, groundLayer);
        }
        else //目前只有玩家能遇到，鄧牆狀態
        {
            //faceDir = new Vector2(-transform.localScale.x, -1);
            isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0,1), checkRaduis, groundLayer);
        }
            
       
        
        //牆體判斷
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);

        //蹬牆中判斷
        if(isPlayer)
            onWall = (touchLeftWall && playerController.inputDirection.x<0f  || touchRightWall && playerController.inputDirection.x>0f) && rb.velocity.y < 0f;
    }

    //Gizmo輔助線
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + (bottomOffset), checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
