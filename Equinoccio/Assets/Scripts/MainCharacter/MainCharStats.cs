using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainCharStats : MonoBehaviour
{
    public static MainCharStats instance;
    public float maxHealth;
    public float health;
    public float maxStamina;
    public float stamina;
    public float cureItem;
    public bool doubleJump;
    public bool wallJump;
    public bool canFireball;
    public bool canSpear;
    public CharData charData;
    public Animator anim;
    public MonoBehaviour controller;
    public HealItems[] healingObjects;
    public float amount = 20f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        string saveFilePath = Path.Combine(Application.persistentDataPath, "/save.json");

        if (File.Exists(saveFilePath))
        {
            Load();
        }
        else
        {
            maxHealth = 100f;
            maxStamina = 100f;
            health = maxHealth;
            stamina = maxStamina;
            cureItem = 2f;
            doubleJump = false;
            wallJump = false;
            canFireball = false;
            canSpear = false;

            Save(); 
        }
    }

    private void Start()
    {
        charData = SaveSystem.LoadGame();
        transform.position = charData.savePosition;
        maxHealth = 100f;
        maxStamina = 100f;
        anim = GetComponent<Animator>();
        Load();
    }

    public void Save()
    {
        CharData data = new CharData();
       
        data.health = health;
        data.stamina = stamina;
        data.cureItem = cureItem;
        data.doubleJump = doubleJump;
        data.wallJump = wallJump;
        data.canFireball = canFireball;
        data.canSpear = canSpear;
        data.savePosition = transform.position;
        data.sceneName = SceneManager.GetActiveScene().name;
        SaveSystem.SaveGame(data);
    }

    public void fullSave()
    {
        CharData data = new CharData();

        data.health = maxHealth;
        data.stamina = maxStamina;
        data.cureItem = 2f; // Restablecer a 2
        data.doubleJump = doubleJump;
        data.wallJump = wallJump;
        data.canFireball =  canFireball;
        data.canSpear = canSpear;
        data.savePosition = transform.position;
        data.sceneName = SceneManager.GetActiveScene().name;
        SaveSystem.SaveGame(data);
        Load();
    }
   
    public void Load()
    {
        CharData data = SaveSystem.LoadGame();
        if (data != null)
        {
            // Si estamos en otra escena, cargar la correcta
            if (SceneManager.GetActiveScene().name != data.sceneName)
            {
                StartCoroutine(LoadSceneAndRestoreData(data));
            }
            else
            {
                RestoreData(data);
            }
        }
    }
    private IEnumerator LoadSceneAndRestoreData(CharData data)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(data.sceneName);

        // Esperar a que termine de cargar
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Luego restaurar los datos
        RestoreData(data);
    }

    private void RestoreData(CharData data)
    {
        health = data.health;
        stamina = data.stamina;
        cureItem = data.cureItem;
        doubleJump = data.doubleJump;
        wallJump = data.wallJump;
        canFireball = data.canFireball;
        canSpear = data.canSpear;
        transform.position = data.savePosition;
    }

    public void Heal()
    {
        if (health< maxHealth && cureItem > 0)
        {
         health += amount;
            if (health > maxHealth)
            {
             health = maxHealth;
            }
            cureItem -= 1f; 
            HideHealingObjects();
        }

       
    }
    public void RecoverHeals()
    {
        foreach (var obj in healingObjects)
        {
            obj.Show();
        }
    }

    private void HideHealingObjects()
    {
        foreach (var obj in healingObjects)
        {
            if (obj.IsActive())
            {
                obj.Hide();
                break; 
            }
        }
    }

    public void TakeDamage(int damage)
    {

        health -= damage;
        anim.SetTrigger("hurt");
        if (health <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        controller.enabled = false; 
        anim.SetTrigger("death");
        StartCoroutine(death());
        Debug.Log("El jugador ha muerto");
       // SceneManager.LoadScene("gameOver");

    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(3f);
    }
}
