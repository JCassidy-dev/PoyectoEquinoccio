using System;
using JetBrains.Annotations;
using UnityEngine;

public class MainCharController : MonoBehaviour
{
    MainCharPhysicCollisions collisionsMC;
    MainCharMove movement;
    bool grounded;
    bool wallNearLeft;
    bool wallNearRight;
    private void Awake()
    {
        collisionsMC = GetComponent<MainCharPhysicCollisions>();
        movement = GetComponent<MainCharMove>();

    }
    private void Start()
    {
       
    }

    private void Update()
    {
        grounded = collisionsMC.checkGround.collider != null;
        wallNearLeft = collisionsMC.checkWallLeft.collider != null;
        wallNearRight = collisionsMC.checkWallRight.collider != null;
    }

    public void UserInput(string direction, bool isPressed)
    {
        switch (direction)
        {
            case "Up":

                break;
            case "Down":

                break;
            case "Left":
                movement.SetDirection(Vector2.left, isPressed);
                break;
            case "Right":
                movement.SetDirection(Vector2.right, isPressed);
                break;
            default:
                break;
        }
    }
    
}
   
    


     

