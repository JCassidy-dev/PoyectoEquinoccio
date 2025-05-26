using UnityEngine;
using UnityEngine.SceneManagement;

public class irInicio : MonoBehaviour
{
    public string nombreEscena = "FirstScene";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
