using UnityEngine;

public class MainCharMove : MonoBehaviour 
{
    public float speed;
    public Rigidbody2D rb;
    public float directionX;
    public Vector2 currentDirection;
    public Vector2 lastDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        speed = 5f;
        currentDirection = Vector2.zero;
        lastDirection = Vector2.zero;
        directionX = 0f;
    }
    public void SetDirection(Vector2 direction, bool isPressed)
    {
        if (isPressed)
        {
            currentDirection = direction;
            if (currentDirection.x != lastDirection.x)
            {
                directionX = currentDirection.x;
                transform.localScale = new Vector3(directionX, 1.0f, 1.0f);
            }
        }
        else
        {
            lastDirection = currentDirection;
            currentDirection = Vector2.zero;
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = currentDirection * speed;
    }   
}
