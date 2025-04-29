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
        // Detecta las teclas y pasa el input al otro script
        if (Input.GetKey(KeyCode.A))
        {
            mainCharController.SetImput("Up");

        }
        if (Input.GetKey(KeyCode.A))
        {
            mainCharController.SetImput("Left");

        }
        if (Input.GetKey(KeyCode.S))
        {
            mainCharController.SetImput("Down");

        }
        if (Input.GetKey(KeyCode.D))
        {
            mainCharController.SetImput("Right");

        }
        if (Input.GetKey(KeyCode.Escape))
        {
            mainCharController.SetImput("Stop");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            mainCharController.SetImput("Jump");
        }
        if (Input.GetKey(KeyCode.J))
        {
            mainCharController.SetImput("Hit");
        }
        if (Input.GetKey(KeyCode.K))
        {
            mainCharController.SetImput("LHit");
        }
    }
}
