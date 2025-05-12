using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MainCharController : MonoBehaviour
{
    MainCharPhysicCollisions collisionsMC;
    MainCharMove movement;
    MainCharImputs input;
    MainCharAtack atack;
    public bool grounded;
    public bool wallNearLeft;
    public bool wallNearRight; 
    public float xDirection;
    private void Awake()
    {
        collisionsMC = GetComponent<MainCharPhysicCollisions>();
        movement = GetComponent<MainCharMove>();
        input = GetComponent<MainCharImputs>();
        atack = GetComponent<MainCharAtack>();

    }
    private void Start()
    {
       
    }

    private void Update()
    {
        xDirection = input.xDirection;
       
        wallNearLeft = collisionsMC.isTouchingLeft;
        Debug.Log("Controlador " + wallNearLeft +" left");
        wallNearRight = collisionsMC.isTouchingRight;
        Debug.Log("Controlador " + wallNearRight + " right");
    }
    private void FixedUpdate()
    {
      grounded = collisionsMC.grounded;
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
            case "AtkM":
            case "AtkRg":
            case "AtkSp":
                atack.Attack(direction);
                break;
            default:
                break;

        }
    }
    
}
   
    


     

