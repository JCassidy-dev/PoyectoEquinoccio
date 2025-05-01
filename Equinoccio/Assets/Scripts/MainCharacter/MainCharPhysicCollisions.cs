using UnityEngine;

public class MainCharPhysicCollisions : MonoBehaviour 
{
    
    RaycastHit2D checkGround; 
    RaycastHit2D checkWallLeft;
    RaycastHit2D checkWallRight; 
    float distanceToCollisionGround; 
    float distanceToCollisionWall; 
    Vector2 colliderCenter;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] CapsuleCollider2D capsuleCollider;

    public LayerMask wallMask;
    
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        colliderCenter = boxCollider.bounds.center;
        distanceToCollisionGround = 1.78f;
        distanceToCollisionWall = 1.44f;
    }

    private void Update()
    {
        colliderCenter = boxCollider.bounds.center; 
        checkGround = Physics2D.Raycast(colliderCenter, Vector2.down, distanceToCollisionGround);
        checkWallLeft= Physics2D.Raycast(colliderCenter, Vector2.left, distanceToCollisionWall, wallMask);
        checkWallRight= Physics2D.Raycast(colliderCenter, Vector2.right, distanceToCollisionWall, wallMask);

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
