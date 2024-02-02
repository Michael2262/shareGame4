using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�@�w�n�����ե�A�S�����|�۰ʲK�[
//[RequireComponent(typeof(CapsuleCollider2D),typeof(BoxCollider2D))]

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    private PlayerController playerController;
    private Rigidbody2D rb;

    [Header("�˴��Ѽ�")]
    public bool manual;
    //���ǪF��u���b���a���⨭�W�~�n�ҥΡA�W�[��ʶ}��
    public bool isPlayer;
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
    public bool onWall;


    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        

        if (!manual) 
        {
            //�]��ء]Bounds�^
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            //�����ϥΤW�@�檺��
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
            
        }
        //�u���b���a���⨭�W�~�n�ҥΡA�H��ʶ}��
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
        //�˴��a��
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + (bottomOffset* -faceDir.x), checkRaduis, groundLayer);
        
        //����P�_
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);

        //���𤤧P�_
        if(isPlayer)
            onWall = (touchLeftWall &&playerController.inputDirection.x<0f  || touchRightWall && playerController.inputDirection.x>0f) && rb.velocity.y < 0f;
    }

    //Gizmo���U�u
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + (bottomOffset * -faceDir.x), checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
