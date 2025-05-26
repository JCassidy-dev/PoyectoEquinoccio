using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroeController : MonoBehaviour
{
    private HeroeMovement playerMovement;
    private HeroeAttack playerAttack;
    private HeroeStats playerStats;
    float lastHit = 0;
    float Invulneravility = 1f;
    public float vidaMaxima, vidaActual;
    float pwUp = 7f;
    float puntos = 0;
    void Start()
    {
        playerMovement = GetComponent<HeroeMovement>();
        playerAttack = GetComponent<HeroeAttack>();
        playerStats = GetComponent<HeroeStats>();
        vidaMaxima = playerStats.maxHealth;
        vidaActual = vidaMaxima;

        
    }

    void Update()
    {
        playerMovement.Update(); 
        playerAttack.Update(); // Llamar al script de ataque
        vidaActual = playerStats.health;
    }

    // Aquí podrías incluir lógica para gestionar el estado del personaje
    public void ApplyDamage(float Xatack, int damage)
    {
        if (Time.time > lastHit + Invulneravility)
        {
            float xplayer = playerMovement.PlayerXSituation;
            int dir = 0;
           if (xplayer - Xatack < 0)
            {
                dir = 1;
            } else
            {
                dir = -1;
            }
           lastHit = Time.time;
           playerStats.TakeDamage(damage);
            playerMovement.SetDamage(dir);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("door"))
        {
            SceneManager.LoadScene("BossFight");
        }
        if (collision.CompareTag("star"))
        {
            StartCoroutine(invulnerable());
            collision.gameObject.GetComponent<fruit>().Die();
        }
    }

    public void Heal(float amount)
    {
        playerStats.Heal(amount);
    }

    public IEnumerator invulnerable()
    {
        Invulneravility = 7f;
        lastHit = Time.time;
        yield return new WaitForSeconds(pwUp);
        Invulneravility = 1f;
    }

    public void sumarPuntos ( float puntosSumar)
    { 
        puntos = puntosSumar + puntos;
    }
}
