using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class MainCharPhysicCollisions : MonoBehaviour 
{
    
    public RaycastHit2D checkGround;
    public RaycastHit2D checkWallLeft;
    public RaycastHit2D checkWallRight; 
    float distanceToCollisionGround; 
    float distanceToCollisionWall;
    float horizontal;
    float horizontalOrigin;
    Vector2 colliderCenter;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] CapsuleCollider2D capsuleCollider;



    public LayerMask groundMask;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();


    }
    private void Start()
    {
       
        colliderCenter = boxCollider.bounds.center;
        distanceToCollisionGround = 1.78f;
        distanceToCollisionWall = 1.44f;
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        colliderCenter = boxCollider.bounds.center; 
        checkGround = Physics2D.Raycast(colliderCenter, Vector2.down, distanceToCollisionGround);
        Debug.Log("Detector Inicial: " + checkGround.ToString());
        checkWallLeft= Physics2D.Raycast(colliderCenter, Vector2.left, distanceToCollisionWall, groundMask);
        checkWallRight= Physics2D.Raycast(colliderCenter, Vector2.right, distanceToCollisionWall, groundMask);

    }

    private void OnDrawGizmos()
    {
        colliderCenter = boxCollider.bounds.center;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(colliderCenter, colliderCenter + Vector2.down * distanceToCollisionGround);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(colliderCenter, colliderCenter + Vector2.left * distanceToCollisionWall);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(colliderCenter, colliderCenter + Vector2.right * distanceToCollisionWall);
    }
}
