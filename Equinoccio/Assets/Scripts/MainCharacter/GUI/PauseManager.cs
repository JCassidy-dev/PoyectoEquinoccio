using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button firstSelectedButton; // Asigna en Inspector el botón que quieres seleccionar al pausar
    private bool isPaused = false;
    public MonoBehaviour playerController;

    private void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            Debug.Log("Pausar");
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
               
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        playerController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

       
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        playerController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Limpiar selección para evitar que quede algo seleccionado cuando el menú está cerrado
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}

