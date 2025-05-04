using System;
using UnityEngine;

public class MainCharImputs : MonoBehaviour
{
    private MainCharController mainCharController;
    public float xDirection;
    void Awake()
    {
        mainCharController = GetComponent<MainCharController>();
        xDirection = 1;
    }

    void Update()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        CheckKey(KeyCode.W, "Up");
        CheckKey(KeyCode.S, "Down");
        CheckKey(KeyCode.A, "Left");
        CheckKey(KeyCode.D, "Right");
        if (Input.GetKeyDown(KeyCode.Space))
            mainCharController.UserInput("Jump", true);
        if (Input.GetKeyDown(KeyCode.J))
            mainCharController.UserInput("AtkM", true);
        if (Input.GetKeyDown(KeyCode.K))
            mainCharController.UserInput("AtkRg", true);
    }

    void CheckKey(KeyCode key, string action)
    {
        if (Input.GetKey(key))
        {
            mainCharController.UserInput(action, true); 
        }
        else if (Input.GetKeyUp(key))
        {
            mainCharController.UserInput(action, false); 
        }
    }
}
