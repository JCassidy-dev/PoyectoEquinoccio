using System;
using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private Animator animator;
    private bool Attack = true;
    float cooldownTime = 2f; 
    public static int damage = 2;


    public bool isAtaccking;


    void Start()
    {
        animator = GetComponent<Animator>();
        isAtaccking = false;
    }

    public void Update()
    {
       

    }

    public void AttackHeroe()
    {
                    if (Attack)
            {
               animator.SetTrigger("Attack");
                    Attack = false;

                StartCoroutine(cooldown());
            } 
                    
    }
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        Attack = true;
        
    }


    public void SetAttack()
    {
        Attack = true;
    }

    public int getAttack()
    {
        return damage;
    }
}
