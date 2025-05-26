using UnityEngine;

public class HeroeAttack : MonoBehaviour
{
    private Animator animator;
    //public int combo = 0;
    private bool Attack = true;
    float cooldownTime = 0.4f; // El tiempo de espera entre acciones (en segundos)
    float lastInputTime = 0f;
    public static int damage = 2;
    AudioSource hit;
    HeroeStats stats;


    void Start()
    {
        animator = GetComponent<Animator>();
        hit = GetComponent<AudioSource>();
        stats = GetComponent<HeroeStats>();
    }

    public void Update()
    {
        if (stats.isAlive)
        {
             if (Input.GetKeyDown(KeyCode.J))
                    {
                        if (Attack  && Time.time >= lastInputTime + cooldownTime)
                        {
                            animator.SetTrigger("Attack");
                            hit.Play();
                            lastInputTime = Time.time;
                            Attack = false;
                        } 
                    }
        }
       
    }

    /*public void EndCombo()
    {
            Attack = false;
            combo = 0; 
    }*/

    public void SetAttack()
    {
        Attack = true;
    }

    public int getAttack()
    {
        return damage;
    }
}



