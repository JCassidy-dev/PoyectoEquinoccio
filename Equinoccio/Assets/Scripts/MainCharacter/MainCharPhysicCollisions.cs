using System;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.EventSystems.EventTrigger;

public class MainCharPhysicCollisions : MonoBehaviour 
{
    
    public RaycastHit2D checkGround;
    public RaycastHit2D checkWallLeft;
    public RaycastHit2D checkWallRight; 
    float distanceToCollisionGround; 
    float distanceToCollisionWall;
    float horizontal;
    [SerializeField] float pixelDesviation; 
    bool isGroundedLeft;
    bool isGroundedCenter;
    bool isGroundedRight;
    Vector2 originLeft;
    Vector2 originCenter; 
    Vector2 originRight;
    public bool grounded;
    Vector2 colliderCenter;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] CapsuleCollider2D capsuleCollider;
    float centerX;
    float bottomY;
    float leftX;
    float rightX;
    float centerY;
    bool isTouchingLeft;
    bool isTouchingRight;
    Vector2 rightTop;
    Vector2 rightCenter;
    Vector2 rightBottom;
    Vector2 leftTop;
    Vector2 leftCenter;
    Vector2 leftBottom;
    public LayerMask groundMask;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        grounded = false;
        isTouchingLeft = false;
        isTouchingRight = false;
        groundMask = LayerMask.GetMask("ground");
    }
    private void Start()
    {
       
        colliderCenter = boxCollider.bounds.center;
        distanceToCollisionGround = 0.2f;
        distanceToCollisionWall = 0.2f;
        pixelDesviation = 0.1f;
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Vector2 boundsMin = boxCollider.bounds.min;
        Vector2 boundsMax = boxCollider.bounds.max;
        centerX = (boundsMin.x + boundsMax.x) / 2f;
        bottomY = boundsMin.y;
        leftX = boundsMin.x;
        rightX = boundsMax.x;
        centerY = (boundsMin.y + boundsMax.y) / 2f;

        originLeft = new Vector2(boundsMin.x +  pixelDesviation, bottomY);
        originCenter = new Vector2(centerX, bottomY);
        originRight = new Vector2(boundsMax.x - pixelDesviation, bottomY);

        //Down
        RaycastHit2D hitLeft = Physics2D.Raycast(originLeft, Vector2.down, distanceToCollisionGround);
        isGroundedLeft = hitLeft.collider != null;

        RaycastHit2D hitCenter = Physics2D.Raycast(originCenter, Vector2.down, distanceToCollisionGround);
        isGroundedCenter = hitCenter.collider != null;

        RaycastHit2D hitRight = Physics2D.Raycast(originRight, Vector2.down, distanceToCollisionGround);
        isGroundedRight = hitRight.collider != null;

        grounded = isGroundedLeft || isGroundedCenter || isGroundedRight;

        //Latheral
        rightTop = new Vector2(rightX , boundsMax.y - pixelDesviation);
        rightCenter = new Vector2(rightX, centerY);
        rightBottom = new Vector2(rightX, boundsMin.y + pixelDesviation);

        leftTop = new Vector2(leftX, boundsMax.y - pixelDesviation);
        leftCenter = new Vector2(leftX, centerY);
        leftBottom = new Vector2(leftX, boundsMin.y + pixelDesviation);

        // right
        RaycastHit2D hitRight1 = Physics2D.Raycast(rightTop, Vector2.right, distanceToCollisionWall, groundMask);
        RaycastHit2D hitRight2 = Physics2D.Raycast(rightCenter, Vector2.right, distanceToCollisionWall, groundMask);
        RaycastHit2D hitRight3 = Physics2D.Raycast(rightBottom, Vector2.right, distanceToCollisionWall, groundMask);

        // left
        RaycastHit2D hitLeft1 = Physics2D.Raycast(leftTop, Vector2.left, distanceToCollisionWall, groundMask);
        RaycastHit2D hitLeft2 = Physics2D.Raycast(leftCenter, Vector2.left, distanceToCollisionWall, groundMask);
        RaycastHit2D hitLeft3 = Physics2D.Raycast(leftBottom, Vector2.left, distanceToCollisionWall, groundMask);

        isTouchingRight = hitRight1 || hitRight2 || hitRight3;
        isTouchingLeft = hitLeft1 || hitLeft2 || hitLeft3;
    }

    private void OnDrawGizmos()
    {
        colliderCenter = boxCollider.bounds.center;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(originLeft, originLeft + Vector2.down * distanceToCollisionGround);
        Gizmos.DrawLine(originCenter, originCenter + Vector2.down * distanceToCollisionGround);
        Gizmos.DrawLine(originRight, originRight + Vector2.down * distanceToCollisionGround);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(leftTop, leftTop + Vector2.left * distanceToCollisionWall);
        Gizmos.DrawLine(leftCenter, leftCenter + Vector2.left * distanceToCollisionWall);
        Gizmos.DrawLine(leftBottom, leftBottom + Vector2.left * distanceToCollisionWall);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(rightTop, rightTop + Vector2.right * distanceToCollisionWall);
        Gizmos.DrawLine(rightCenter, rightCenter + Vector2.right * distanceToCollisionWall);
        Gizmos.DrawLine(rightBottom, rightBottom + Vector2.right * distanceToCollisionWall);

    }
}
