using System;
using UnityEngine;

public class MainCharImputs : MonoBehaviour
{
    private MainCharController mainCharController;

    void Awake()
    {
        // Busca el otro componente en el mismo GameObject
        mainCharController = GetComponent<MainCharController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            mainCharController.UserImput("Up");
        }
        if (Input.GetKey(KeyCode.A))
        {
            mainCharController.UserImput("Left");
        }
        if (Input.GetKey(KeyCode.S))
        {
            mainCharController.UserImput("Down");
        }
        if (Input.GetKey(KeyCode.D))
        {
            mainCharController.UserImput("Right");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
        if (Input.GetKey(KeyCode.J))
        {
            mainCharController.UserImput("AtkM");
        }
        if (Input.GetKey(KeyCode.K))
        {
            mainCharController.UserImput("AtkRg");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            mainCharController.UserImput("Jump");
        }
    }
}
