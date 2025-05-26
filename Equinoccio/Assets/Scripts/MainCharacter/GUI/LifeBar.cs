using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LifeBar : MonoBehaviour
{
    public Image barraVida;
    public MainCharStats stats;
    public float maxLife;
   
    void Start()
    {
        stats = GameObject.Find("Hiro").GetComponent<MainCharStats>();
        maxLife = stats.maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        barraVida.fillAmount = stats.health / maxLife;
       
    }
}
