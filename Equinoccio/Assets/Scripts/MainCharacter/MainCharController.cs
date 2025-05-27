using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
    public float looking;
    public float stamina;
    MainCharStats stats;
    float lastHit = 0;
    float Invulneravility = 1f;
    bool savepoint;
    
    private void Awake()
    {
        collisionsMC = GetComponent<MainCharPhysicCollisions>();
        movement = GetComponent<MainCharMove>();
        input = GetComponent<MainCharImputs>();
        atack = GetComponent<MainCharAtack>();
        stats = GetComponent<MainCharStats>();

    }
    private void Start()
    {
       
    }

    private void Update()
    {
        xDirection = input.xDirection;
        looking = movement.looking;
        wallNearLeft = collisionsMC.isTouchingLeft;
        Debug.Log("Controlador " + wallNearLeft +" left");
        wallNearRight = collisionsMC.isTouchingRight;
        Debug.Log("Controlador " + wallNearRight + " right");
        stamina = stats.stamina;
        savepoint = collisionsMC.Savepoint;
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
                if (savepoint && isPressed)
                {
                    stats.fullSave();
                    Debug.Log("Savepoint reached, game saved.");
                }
                break;
            case "Down":
                stats.Load();
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
            case "Heal":
                stats.Heal();
                break;
            default:
                break;

        }
    }

    public void decreaseStamina(float staminaLess)
    {
        stats.stamina -= staminaLess;
    }

    public void increaseStamina(float staminaMore)
    {
        if (stats.stamina + staminaMore < stats.maxStamina)
        {
             stats.stamina += staminaMore;
        } else
        {
            stats.stamina = stats.maxStamina;
        }
       
    }

    public void ApplyDamage(float Xatack, int damage)
    {
        if (Time.time > lastHit + Invulneravility)
        {
            float xplayer = transform.position.x;
            int dir = 0;
            if (xplayer - Xatack < 0)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
            lastHit = Time.time;
            stats.TakeDamage(damage);
            movement.SetDamage(dir);
        }
    }


}
   
    


     

