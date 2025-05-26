using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Unity.VisualScripting;

public class StaminaBar : MonoBehaviour
{
    [SerializeField]  public Image barraStamina;
    public MainCharStats stats;
    public float maxStamina;
    
    void Start()
    {
        stats = GameObject.Find("Hiro").GetComponent<MainCharStats>();
        maxStamina = stats.maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        barraStamina.fillAmount = stats.stamina / maxStamina;
    }
}
