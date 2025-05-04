using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MainCharController : MonoBehaviour
{
    MainCharPhysicCollisions collisionsMC;
    MainCharMove movement;
    MainCharImputs input;
    public bool grounded;
    public bool wallNearLeft;
    public bool wallNearRight; 
    public float xDirection;
    private void Awake()
    {
        collisionsMC = GetComponent<MainCharPhysicCollisions>();
        movement = GetComponent<MainCharMove>();
        input = GetComponent<MainCharImputs>();

    }
    private void Start()
    {
       
    }

    private void Update()
    {
        xDirection = input.xDirection;
        grounded = collisionsMC.checkGround.collider != null;
        Debug.Log(grounded);
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
            case "Right":
                movement.SetDirection(xDirection, isPressed); 
                break;
            case "Jump":
                movement.JumpPress();
                break;
            default:
                break;

        }
    }
    
}
   
    


     

