using UnityEngine;

public class SwordAndSpear : MonoBehaviour
{
    public float damageSword;
    public float damageSpear;
    public float increaseDamage;
    public float moreStamina;
    MainCharController controller;
    // public bool berserkTime; revisar igual no va aquí
    void Start()
    {
        damageSword = 10f;
        damageSpear = 15f;
        increaseDamage = 1.75f;
        moreStamina = 5f;
        controller = GetComponent<MainCharController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getSwordDamage()
    {
        controller.increaseStamina(moreStamina);
        return damageSword;
    }

    public float getSpearDamage()
    {
        return damageSpear;
    }


}
